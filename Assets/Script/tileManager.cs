using UnityEngine;
using UnityEngine.Tilemaps;

public class tileManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactedTile;

    void Start()
    {
        foreach(var position in interactableMap.cellBounds.allPositionsWithin)
        {
            interactableMap.SetTile(position, hiddenInteractableTile);
        }
    }
    public bool IsInteractable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);
        if(tile != null)
        {
            if(tile  == hiddenInteractableTile)
            {
                return true;
            }
        }
        return false;
    }
    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, interactedTile); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
