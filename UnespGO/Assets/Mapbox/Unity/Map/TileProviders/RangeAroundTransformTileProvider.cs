using System.Collections.Generic;
using Mapbox.Map;
using UnityEngine;

namespace Mapbox.Unity.Map.TileProviders
{
	public class RangeAroundTransformTileProvider : AbstractTileProvider
	{
		[SerializeField] private RangeAroundTransformTileProviderOptions _rangeTileProviderOptions;

		private bool _initialized = false;
		private UnwrappedTileId _currentTile;
		private bool _waitingForTargetTransform = false;

		public override void OnInitialized()
		{
			if (Options != null)
			{
				_rangeTileProviderOptions = (RangeAroundTransformTileProviderOptions)Options;
			}
			else if (_rangeTileProviderOptions == null)
			{
				_rangeTileProviderOptions = new RangeAroundTransformTileProviderOptions();
			}

			if (_rangeTileProviderOptions.targetTransform == null)
			{
				Debug.LogWarning("TransformTileProvider: No location marker transform specified. Assigning default transform.");
				_rangeTileProviderOptions.targetTransform = FindDefaultTargetTransform();
				_waitingForTargetTransform = _rangeTileProviderOptions.targetTransform == null;
			}
			else
			{
				_initialized = true;
			}
			_currentExtent.activeTiles = new HashSet<UnwrappedTileId>();
			_map.OnInitialized += UpdateTileExtent;
			_map.OnUpdated += UpdateTileExtent;
		}

		private Transform FindDefaultTargetTransform()
		{
			// Attempt to find a default transform in the scene
			GameObject defaultTarget = GameObject.FindWithTag("Player"); // Example: Look for a GameObject tagged as "Player"
			return defaultTarget != null ? defaultTarget.transform : null;
		}

		public override void UpdateTileExtent()
		{
			if (!_initialized) return;

			_currentExtent.activeTiles.Clear();
			_currentTile = TileCover.CoordinateToTileId(_map.WorldToGeoPosition(_rangeTileProviderOptions.targetTransform.localPosition), _map.AbsoluteZoom);

			Debug.Log($"Current Tile: {_currentTile.X}, {_currentTile.Y}, Zoom: {_map.AbsoluteZoom}");

			for (int x = _currentTile.X - _rangeTileProviderOptions.visibleBuffer; x <= (_currentTile.X + _rangeTileProviderOptions.visibleBuffer); x++)
			{
				for (int y = _currentTile.Y - _rangeTileProviderOptions.visibleBuffer; y <= (_currentTile.Y + _rangeTileProviderOptions.visibleBuffer); y++)
				{
					Debug.Log($"Adding Tile: X={x}, Y={y}");
					_currentExtent.activeTiles.Add(new UnwrappedTileId(_map.AbsoluteZoom, x, y));
				}
			}
			OnExtentChanged();
		}

		public override void UpdateTileProvider()
		{
			if (_waitingForTargetTransform && !_initialized)
			{
				if (_rangeTileProviderOptions.targetTransform != null)
				{
					_initialized = true;
				}
			}

			if (_rangeTileProviderOptions != null && _rangeTileProviderOptions.targetTransform != null && _rangeTileProviderOptions.targetTransform.hasChanged)
			{
				UpdateTileExtent();
				_rangeTileProviderOptions.targetTransform.hasChanged = false;
			}
		}

		public override bool Cleanup(UnwrappedTileId tile)
		{
			bool dispose = false;
			dispose = tile.X > _currentTile.X + _rangeTileProviderOptions.disposeBuffer || tile.X < _currentTile.X - _rangeTileProviderOptions.disposeBuffer;
			dispose = dispose || tile.Y > _currentTile.Y + _rangeTileProviderOptions.disposeBuffer || tile.Y < _currentTile.Y - _rangeTileProviderOptions.disposeBuffer;


			return (dispose);
		}
	}
}