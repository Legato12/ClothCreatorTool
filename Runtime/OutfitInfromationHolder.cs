using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class OutfitInfromationHolder1 : MonoBehaviour
{
    [Serializable]
    public class ClothPart
    {
        public SpritePartLayers spritePartLayers = new();
        public SpriteRenderer partRenderer;
    }

    [Space] public Transform spriteSetsParent;

    [Space] public ClothPart torsoSpritePartLayers;

    [Space] public ClothPart armLeftSpritePartLayers;
    public ClothPart armLeftLowerSpritePartLayers;

    public ClothPart armRightSpritePartLayers;
    public ClothPart armRightLowerSpritePartLayers;

    [Space] public ClothPart hipsSpritePartLayers;

    [Space] public ClothPart legLeftSpritePartLayers;
    public ClothPart legLeftLowerSpritePartLayers;
    public ClothPart legRightSpritePartLayers;
    public ClothPart legRightLowerSpritePartLayers;

    [Space] public ClothPart dressRightMiddleLayers;
    public ClothPart dressLeftMiddleLayers;
    public ClothPart dressRightLayers;
    public ClothPart dressLeftLayers;
    public ClothPart dressMiddleLayers;

    public ClothPart foldedLayers;

    public Color[] tintChannels;

    public int shirtSortingOrder = 80;
    public int skirtSortingOrder = 80;
    public int hipsSortingOrder = 70;
    public int jacketSortingOrder = 100;

    public bool isShirt;
    public bool isJacket;
    public bool isDress;
    public bool isPants;

#if UNITY_EDITOR && MT
    [Button("Add sorting groups")]
    public void AddSortingGroups()
    {
        Debug.Log($"{GetType()} >>> Adding sorting groups for {gameObject.name}");
        gameObject.layer = LayerMask.NameToLayer("L_INTERACTIVE");

        var sortingGroup = GetComponent<SortingGroup>();
        sortingGroup.sortingLayerName = "Default";
        if (isDress)
        {
            if (torsoSpritePartLayers.partRenderer)
            {
                torsoSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    shirtSortingOrder;
            }

            if (armRightSpritePartLayers.partRenderer)
            {
                armRightSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    shirtSortingOrder - 1;
            }

            if (armRightLowerSpritePartLayers.partRenderer)
            {
                armRightLowerSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    shirtSortingOrder - 1;
            }

            if (armLeftSpritePartLayers.partRenderer)
            {
                armLeftSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    shirtSortingOrder - 1;
            }

            if (armLeftLowerSpritePartLayers.partRenderer)
            {
                armLeftLowerSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    shirtSortingOrder - 1;
            }

            if (dressRightMiddleLayers.partRenderer)
            {
                dressRightMiddleLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder;
            }
            if (dressLeftMiddleLayers.partRenderer)
            {
                dressLeftMiddleLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder - 1;
            }
            if (dressRightLayers.partRenderer)
            {
                dressRightLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder - 1;
            }
            if (dressLeftLayers.partRenderer)
            {
                dressLeftLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder - 1;
            }
            if (dressMiddleLayers.partRenderer)
            {
                dressMiddleLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder - 1;
            }

            Debug.Log($"{GetType()} Added sorting groups for shirt {gameObject.name}");
        }
        if (isShirt)
        {
            if (torsoSpritePartLayers.partRenderer)
            {
                torsoSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    shirtSortingOrder;
            }

            if (armRightSpritePartLayers.partRenderer)
            {
                armRightSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    shirtSortingOrder - 1;
            }

            if (armRightLowerSpritePartLayers.partRenderer)
            {
                armRightLowerSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    shirtSortingOrder - 1;
            }

            if (armLeftSpritePartLayers.partRenderer)
            {
                armLeftSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    shirtSortingOrder - 1;
            }

            if (armLeftLowerSpritePartLayers.partRenderer)
            {
                armLeftLowerSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    shirtSortingOrder - 1;
            }

            Debug.Log($"{GetType()} Added sorting groups for shirt {gameObject.name}");
        }

        if (isJacket)
        {
            if (torsoSpritePartLayers.partRenderer)
            {
                torsoSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    jacketSortingOrder;
            }

            if (armRightSpritePartLayers.partRenderer)
            {
                armRightSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    jacketSortingOrder - 1;
            }

            if (armRightLowerSpritePartLayers.partRenderer)
            {
                armRightLowerSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    jacketSortingOrder - 1;
            }

            if (armLeftSpritePartLayers.partRenderer)
            {
                armLeftSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    jacketSortingOrder - 1;
            }

            if (armLeftLowerSpritePartLayers.partRenderer)
            {
                armLeftLowerSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                    jacketSortingOrder - 1;
            }

            Debug.Log($"{GetType()} Added sorting groups for jacket {gameObject.name}");
        }

        if (!isPants) return;

        if (hipsSpritePartLayers.partRenderer)
        {
            hipsSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                hipsSortingOrder;
        }

        if (legLeftSpritePartLayers.partRenderer)
        {
            legLeftSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                hipsSortingOrder - 1;
        }

        if (legLeftLowerSpritePartLayers.partRenderer)
        {
            legLeftLowerSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                hipsSortingOrder + 1;
        }

        if (legRightSpritePartLayers.partRenderer)
        {
            legRightSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                hipsSortingOrder - 1;
        }

        if (legRightLowerSpritePartLayers.partRenderer)
        {
            legRightLowerSpritePartLayers.partRenderer.gameObject.AddComponent<SortingGroup>().sortingOrder =
                hipsSortingOrder + 1;
        }

        Debug.Log($"{GetType()} >>> Added sorting groups for pants {gameObject.name}");
    }

    [Button("Update sorting orders")]
    public void UpdateSortingOrders()
    {
        Debug.Log($"{GetType()} >>> Updating sorting groups for {gameObject.name}");
        gameObject.layer = LayerMask.NameToLayer("L_INTERACTIVE");

        var sortingGroup = GetComponent<SortingGroup>();
        sortingGroup.sortingLayerName = "Default";

        if (isDress)
        {
            if (torsoSpritePartLayers.partRenderer)
            {
                torsoSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    shirtSortingOrder;
            }

            if (armRightSpritePartLayers.partRenderer)
            {
                armRightSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder - 1;
            }

            if (armRightLowerSpritePartLayers.partRenderer)
            {
                armRightLowerSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder - 1;
            }

            if (armLeftSpritePartLayers.partRenderer)
            {
                armLeftSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder - 1;
            }

            if (armLeftLowerSpritePartLayers.partRenderer)
            {
                armLeftLowerSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder - 1;
            }
            if (dressRightMiddleLayers.partRenderer)
            {
                dressRightMiddleLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder;
            }
            if (dressLeftMiddleLayers.partRenderer)
            {
                dressLeftMiddleLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder - 1;
            }
            if (dressRightLayers.partRenderer)
            {
                dressRightLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder - 1;
            }
            if (dressLeftLayers.partRenderer)
            {
                dressLeftLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder - 1;
            }
            if (dressMiddleLayers.partRenderer)
            {
                dressMiddleLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    skirtSortingOrder - 1;
                Debug.Log($"{GetType()} Updated sorting groups for dress {gameObject.name}");
            }
            if (isShirt)
            {
                if (torsoSpritePartLayers.partRenderer)
                {
                    torsoSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                        shirtSortingOrder;
                }

                if (armRightSpritePartLayers.partRenderer)
                {
                    armRightSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                        shirtSortingOrder - 1;
                }

                if (armRightLowerSpritePartLayers.partRenderer)
                {
                    armRightLowerSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                        shirtSortingOrder - 1;
                }

                if (armLeftSpritePartLayers.partRenderer)
                {
                    armLeftSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                        shirtSortingOrder - 1;
                }

                if (armLeftLowerSpritePartLayers.partRenderer)
                {
                    armLeftLowerSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                        shirtSortingOrder - 1;
                }

                Debug.Log($"{GetType()} Updated sorting groups for shirt {gameObject.name}");
            }

            if (isJacket)
            {
                if (torsoSpritePartLayers.partRenderer)
                {
                    torsoSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                        jacketSortingOrder;
                }

                if (armRightSpritePartLayers.partRenderer)
                {
                    armRightSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                        jacketSortingOrder - 1;
                }

                if (armRightLowerSpritePartLayers.partRenderer)
                {
                    armRightLowerSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                        jacketSortingOrder - 1;
                }

                if (armLeftSpritePartLayers.partRenderer)
                {
                    armLeftSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                        jacketSortingOrder - 1;
                }

                if (armLeftLowerSpritePartLayers.partRenderer)
                {
                    armLeftLowerSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                        jacketSortingOrder - 1;
                }

                Debug.Log($"{GetType()} Updated sorting groups for jacket {gameObject.name}");
            }

            if (!isPants) return;

            if (hipsSpritePartLayers.partRenderer)
            {
                hipsSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    hipsSortingOrder;
            }

            if (legLeftSpritePartLayers.partRenderer)
            {
                legLeftSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    hipsSortingOrder - 1;
            }

            if (legLeftLowerSpritePartLayers.partRenderer)
            {
                legLeftLowerSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    hipsSortingOrder + 1;
            }

            if (legRightSpritePartLayers.partRenderer)
            {
                legRightSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    hipsSortingOrder - 1;
            }

            if (legRightLowerSpritePartLayers.partRenderer)
            {
                legRightLowerSpritePartLayers.partRenderer.gameObject.GetComponent<SortingGroup>().sortingOrder =
                    hipsSortingOrder + 1;
            }

            Debug.Log($"{GetType()} >>> Updated sorting groups for pants {gameObject.name}");
        }
    }
    [Button("Add Components")]
    public void AddComponents()
    {
        Debug.Log($"{GetType()} >>>Adding components for {gameObject.name}...");

        transform.GetChild(0).gameObject.AddComponent<DHSprites>();
        Debug.Log($"{GetType()} Added {typeof(DHSprites)}");

        gameObject.AddComponent<DHIdentifier>();
        Debug.Log($"{GetType()} Added {typeof(DHIdentifier)}");

        var shareItem = gameObject.AddComponent<DHShareItem>();
        if (shareItem)
        {
            shareItem.shareInformations = new DHShareItem.ShareInformation();
            shareItem.enabledSharing = true;
            Debug.Log($"{GetType()} Added {typeof(DHShareItem)}");
        }

        var physics = gameObject.AddComponent<DHPhysics>();
        if (physics)
        {
            var mask = 1 << LayerMask.NameToLayer("L_FLOOR");
            mask |= 1 << LayerMask.NameToLayer("L_NON_FLOOR");
            mask |= 1 << LayerMask.NameToLayer("L_SHELF");

            physics.placementLayers = mask;
            Debug.Log($"{GetType()} Added {typeof(DHPhysics)}");
        }

        var draggable = gameObject.AddComponent<DHDraggableObject>();
        if (draggable)
        {
            draggable.canBeDestroyed = false;
            draggable.dropSound = DHSoundManager.GeneralDropSoundClipsType.Clothes;
            draggable.socketTags = new[]
            {
                new DHSocketTag(new DHTagsDefinitions.SensitiveType { name = @"Default\CharOutfit" }, Vector2.zero),
                new DHSocketTag(new DHTagsDefinitions.SensitiveType { name = @"Default\CharLeftHand" }, Vector2.zero),
                new DHSocketTag(new DHTagsDefinitions.SensitiveType { name = @"Default\CharRightHand" }, Vector2.zero),
                new DHSocketTag(new DHTagsDefinitions.SensitiveType { name = @"ShelvesAndCabinets\Shelf" },
                    Vector2.zero),
                new DHSocketTag(new DHTagsDefinitions.SensitiveType { name = @"ShelvesAndCabinets\Cabinet" },
                    Vector2.zero)
            };

            Debug.Log($"{GetType()} Added {typeof(DHDraggableObject)}");
        }

        var clothItem = gameObject.AddComponent<DHEClothing>();
        if (!clothItem) return;

        SetupClothItem(clothItem);

        Debug.Log($"{GetType()} Added {typeof(DHEClothing)}");

        Debug.Log($"{GetType()} >>>Added components for {gameObject.name}...");
    }

    [Button("Add wall mounted socket tag")]
    public void SetSocketTags()
    {
        var draggable = gameObject.GetComponent<DHDraggableObject>();
        if (!draggable) return;
        var socketTags = new List<DHSocketTag>(draggable.socketTags);
        socketTags.Add(new DHSocketTag(new DHTagsDefinitions.SensitiveType { name = @"Miscs\WallMountedClothes" }, Vector2.zero));
        draggable.socketTags = socketTags.ToArray();

        Debug.Log($"{GetType()} Set socket tags");
    }

    private void SetupClothItem(DHEClothing clothItem)
    {
        clothItem.dragState = DHEClothItem.BaseClothState.FOLDED;

        SetFoldedReference(clothItem);

        if (isDress)
        {
            clothItem.type = DHEClothItem.ClothType.DRESS;

            SetupTorsoAndArms(clothItem);
            SetupDress(clothItem);
        }
        else if (isShirt)
        {
            clothItem.type = DHEClothItem.ClothType.SHIRT;

            SetupTorsoAndArms(clothItem);
        }
        else if (isJacket)
        {
            clothItem.type = DHEClothItem.ClothType.JACKET;

            SetupTorsoAndArms(clothItem);
        }
        else
        {
            clothItem.type = DHEClothItem.ClothType.PANTS;

            SetupHipsAndLegs(clothItem);
        }
    }

    [Button("Update references")]
    public void UpdateReferences()
    {
        var clothItem = gameObject.GetComponent<DHEClothing>();
        if (!clothItem)
        {
            Debug.LogWarning(
                $"{GetType()} Could not find cloth item, press Add Components if you want to setup a cloth item!");
            return;
        }

        SetupClothItem(clothItem);
    }

    private void SetFoldedReference(DHEClothing clothItem)
    {
        foreach (Transform item in transform.GetChild(0).transform)
        {
            if (!item.name.Contains("Outfit_Folded(Clone)")) continue;

            clothItem.folded = item.gameObject;
            break;
        }
    }
    private void SetupDress(DHEClothing clothItem)
    {
        clothItem.ClothInstancesInfo = new DHEGenericClothInstancesInfo
        {
            Torso = torsoSpritePartLayers.partRenderer.transform,

            ArmLeftUpper = armLeftSpritePartLayers.partRenderer
                ? armLeftSpritePartLayers.partRenderer.transform
                : null,
            ArmLeftLower = armLeftLowerSpritePartLayers.partRenderer
                ? armLeftLowerSpritePartLayers.partRenderer.transform
                : null,

            ArmRightUpper = armRightSpritePartLayers.partRenderer
                ? armRightSpritePartLayers.partRenderer.transform
                : null,
            ArmRightLower = armRightLowerSpritePartLayers.partRenderer
                ? armRightLowerSpritePartLayers.partRenderer.transform
                : null,
            DressLeft = dressLeftLayers.partRenderer
                ? dressLeftLayers.partRenderer.transform
                : null,
            DressRight = dressRightLayers.partRenderer
                ? dressRightLayers.partRenderer.transform
                : null,

            DressLeftMiddle = dressLeftMiddleLayers.partRenderer
                ? dressLeftMiddleLayers.partRenderer.transform
                : null,
            DressRightMiddle = dressRightMiddleLayers.partRenderer
                ? dressRightMiddleLayers.partRenderer.transform
                : null,

           Middle = dressMiddleLayers.partRenderer
                ? dressMiddleLayers.partRenderer.transform
                : null
        };
    }
    private void SetupHipsAndLegs(DHEClothing clothItem)
    {
        clothItem.ClothInstancesInfo = new DHEGenericClothInstancesInfo
        {
            Pants = hipsSpritePartLayers.partRenderer.transform,

            LegRightUpper = legRightSpritePartLayers.partRenderer.transform,
            LegRightLower = legRightLowerSpritePartLayers.partRenderer
                ? legRightLowerSpritePartLayers.partRenderer.transform
                : null,

            LegLeftUpper = legLeftSpritePartLayers.partRenderer.transform,
            LegLeftLower = legLeftLowerSpritePartLayers.partRenderer
                ? legLeftLowerSpritePartLayers.partRenderer.transform
                : null
        };

        if (clothItem.ClothInstancesInfo.LegLeftUpper != null)
        {
            clothItem.ClothInstancesInfo.LegLeftUpper.GetComponent<SpriteRenderer>().flipX = true;

            foreach (Transform item in clothItem.ClothInstancesInfo.LegLeftUpper.transform)
            {
                item.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        if (clothItem.ClothInstancesInfo.LegLeftLower)
        {
            clothItem.ClothInstancesInfo.LegLeftLower.GetComponent<SpriteRenderer>().flipX = true;

            foreach (Transform item in clothItem.ClothInstancesInfo.LegLeftLower.transform)
            {
                item.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }

    private void SetupTorsoAndArms(DHEClothing clothItem)
    {
        clothItem.ClothInstancesInfo = new DHEGenericClothInstancesInfo
        {
            Torso = torsoSpritePartLayers.partRenderer.transform,

            ArmLeftUpper = armLeftSpritePartLayers.partRenderer
                ? armLeftSpritePartLayers.partRenderer.transform
                : null,
            ArmLeftLower = armLeftLowerSpritePartLayers.partRenderer
                ? armLeftLowerSpritePartLayers.partRenderer.transform
                : null,

            ArmRightUpper = armRightSpritePartLayers.partRenderer
                ? armRightSpritePartLayers.partRenderer.transform
                : null,
            ArmRightLower = armRightLowerSpritePartLayers.partRenderer
                ? armRightLowerSpritePartLayers.partRenderer.transform
                : null
        };
    }
#endif
}