using UnityEngine;
using UnityEngine.Tilemaps;

public class tileManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile plowedTile;

    void Start()
    {
        foreach(var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);
            if(tile!= null && tile.name == "Interactable1")
            {

                interactableMap.SetTile(position, hiddenInteractableTile);
            }

 
        }
    }
  
    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, plowedTile); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public string GetTileName(Vector3Int position)
    {
        if(interactableMap != null)
        {
            TileBase tile = interactableMap.GetTile(position);
            if(tile != null)
            {
                return tile.name;
            }
        }
        return "";
    }
}
