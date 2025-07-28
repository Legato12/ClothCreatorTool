#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ClothCreatorPrefabController : MonoBehaviour
{

    [Serializable]
    public class ClothControllers
    {
        public List<ClothCreatorSpritePivotController> torsoLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> armRightLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> armLowerRightLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> armLeftLayer = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> armLowerLeftLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> hipsLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> legRightLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> legLowerRightLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> legLeftLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> legLowerLeftLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> dressRightMiddleLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> dressLeftMiddleLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> dressRightLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> dressLeftLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> dressMiddleLayers = new List<ClothCreatorSpritePivotController>();
        public List<ClothCreatorSpritePivotController> foldedLayers = new List<ClothCreatorSpritePivotController>();

        public List<List<ClothCreatorSpritePivotController>> EveryLayer
        {
            get
            {
                if (_everyLayer == null)
                    _everyLayer = new List<List<ClothCreatorSpritePivotController>>
                    { torsoLayers, armRightLayers, armLowerRightLayers, armLeftLayer, armLowerLeftLayers, hipsLayers, legRightLayers, legLowerRightLayers, legLeftLayers, legLowerLeftLayers,
                    dressRightMiddleLayers, dressLeftMiddleLayers, dressRightLayers, dressLeftLayers, dressMiddleLayers, foldedLayers};
                return _everyLayer;
            }
        }

        private List<List<ClothCreatorSpritePivotController>> _everyLayer;
    }

    private const string ClothesFolderPath = "Assets/Character-Assets/Prefabs/AnimatedClothes";
    public ClothControllers[] clothes = new ClothControllers[3];

    public OutfitInfromationHolder clothPrefab;
    public ClothCreatorSpritePivotController layerPrefab;

    //public List<ClothCreatorSpritePivotController> ShirtControllers
    //{
    //    get
    //    {
    //        if (_shirtControllers == null || _shirtControllers.Count == 0)
    //            _shirtControllers = new List<ClothCreatorSpritePivotController> { torso, armRight, armLowerRight, armLeft, armLowerLeft};
    //        return _shirtControllers;
    //    }
    //}

    public Transform torso;
    public Transform armRight;
    public Transform armLowerRight;
    public Transform armLeft;
    public Transform armLowerLeft;
    public Transform hips;
    public Transform legRight;
    public Transform legLowerRight;
    public Transform legLeft;
    public Transform legLowerLeft;
    public Transform dressRightMiddle;
    public Transform dressLeftMiddle;
    public Transform dressRight;
    public Transform dressLeft;
    public Transform dressMiddle;
    public Transform folded;


    private List<ClothCreatorSpritePivotController> _shirtControllers;
    private Dictionary<string, List<ClothCreatorSpritePivotController>> _clothLayers = new Dictionary<string, List<ClothCreatorSpritePivotController>>();

    private void UpdateSpriteControllers(List<ClothCreatorSpritePivotController> spriteControllers, SpritePartLayers.SpritePart[] sprites, Transform parent, Color color, bool changeColor = true)
    {
        var transformCount = spriteControllers.Count;
        var spriteCount = sprites.Length;

        var difference = spriteCount - transformCount;

        if (difference > 0)
        {
            for (int i = 0; i < difference; i++)
            {
                var spriteController = Instantiate(layerPrefab);

                spriteController.transform.SetParent(spriteControllers.Count == 0 ? parent : spriteControllers[0].transform);
                
                var parentSpriteRenderer = spriteController.transform.parent.GetComponent<SpriteRenderer>();
                if (parentSpriteRenderer != null)
                {
                    spriteController.SpriteRenderer.sortingOrder = parentSpriteRenderer.sortingOrder + 1 + spriteController.transform.GetSiblingIndex();
                }

                spriteController.transform.localPosition = Vector3.zero;
                spriteController.transform.localEulerAngles = Vector3.zero;
                spriteController.transform.localScale = Vector3.one;
                if (spriteControllers.Count == 0)
                    spriteController.gameObject.name = "Outfit_" + parent.name;
                else
                    spriteController.gameObject.name = "Outfit_" + parent.name + "_Layer_ " + spriteController.transform.GetSiblingIndex();

                spriteControllers.Add(spriteController);
            }
        }
        else if (difference < 0)
        {
            for (int i = 0; i < difference * -1; i++)
            {
                DestroyImmediate(spriteControllers[spriteControllers.Count - 1].gameObject);
                spriteControllers.RemoveAt(spriteControllers.Count - 1);
            }
        }
        for (int i = 0; i < spriteControllers.Count; i++)
        {
            spriteControllers[i].SetSprite(sprites[i].sprite);
            spriteControllers[i].colorChannelIndex = sprites[i].tintChannel;
            if (changeColor == false)
                continue;
            spriteControllers[i].SetColor(color);
        }
    }

    public void FindAndSetSprites(int clothIndex, string partName, SpritePartLayers.SpritePart[] spriteParts, Color color, bool changeColor = true)
    {

        switch (partName)
        {
            case ClothNames.TorsoName:
                UpdateSpriteControllers(clothes[clothIndex].torsoLayers, spriteParts, torso, color, changeColor);
                break;
            case ClothNames.ArmLeftName:
                UpdateSpriteControllers(clothes[clothIndex].armLeftLayer, spriteParts, armLeft, color, changeColor);
                break;
            case ClothNames.ArmLeftLowerName:
                UpdateSpriteControllers(clothes[clothIndex].armLowerLeftLayers, spriteParts, armLowerLeft, color, changeColor);
                break;
            case ClothNames.ArmRightName:
                UpdateSpriteControllers(clothes[clothIndex].armRightLayers, spriteParts, armRight, color, changeColor);
                break;
            case ClothNames.ArmLowerRightName:
                UpdateSpriteControllers(clothes[clothIndex].armLowerRightLayers, spriteParts, armLowerRight, color, changeColor);
                break;
            case ClothNames.HipsName:
                UpdateSpriteControllers(clothes[clothIndex].hipsLayers, spriteParts, hips, color, changeColor);
                break;
            case ClothNames.LegLeftName:
                UpdateSpriteControllers(clothes[clothIndex].legLeftLayers, spriteParts, legLeft, color, changeColor);
                break;
            case ClothNames.LegLowerLeftName:
                UpdateSpriteControllers(clothes[clothIndex].legLowerLeftLayers, spriteParts, legLowerLeft, color, changeColor);
                break;
            case ClothNames.LegRightName:
                UpdateSpriteControllers(clothes[clothIndex].legRightLayers, spriteParts, legRight, color, changeColor);
                break;
            case ClothNames.LegLowerRightName:
                UpdateSpriteControllers(clothes[clothIndex].legLowerRightLayers, spriteParts, legLowerRight, color, changeColor);
                break;
            case ClothNames.DressRightMiddle:
                UpdateSpriteControllers(clothes[clothIndex].dressRightMiddleLayers, spriteParts, dressRightMiddle, color, changeColor);
                break;
            case ClothNames.DressLeftMiddle:
                UpdateSpriteControllers(clothes[clothIndex].dressLeftMiddleLayers, spriteParts, dressLeftMiddle, color, changeColor);
                break;
            case ClothNames.DressRight:
                UpdateSpriteControllers(clothes[clothIndex].dressRightLayers, spriteParts, dressRight, color, changeColor);
                break;
            case ClothNames.DressLeft:
                UpdateSpriteControllers(clothes[clothIndex].dressLeftLayers, spriteParts, dressLeft, color, changeColor);
                break;
            case ClothNames.DressMiddle:
                UpdateSpriteControllers(clothes[clothIndex].dressMiddleLayers, spriteParts, dressMiddle, color, changeColor);
                break;
            case ClothNames.FoldedName:
                UpdateSpriteControllers(clothes[clothIndex].foldedLayers, spriteParts, folded, color, changeColor);
                break;

        }
    }

    public void RefreshEverySpriteControllers(int clothIndex)
    {
        var spritePartLayers = new SpritePartLayers { spriteParts = new List<SpritePartLayers.SpritePart>() };
        var spriteParts = spritePartLayers.spriteParts.ToArray();
        var color = Color.white;
        UpdateSpriteControllers(clothes[clothIndex].torsoLayers, spriteParts, torso, color);
        UpdateSpriteControllers(clothes[clothIndex].armLeftLayer, spriteParts, armLeft, color);
        UpdateSpriteControllers(clothes[clothIndex].armLowerLeftLayers, spriteParts, armLowerLeft, color);
        UpdateSpriteControllers(clothes[clothIndex].armRightLayers, spriteParts, armRight, color);
        UpdateSpriteControllers(clothes[clothIndex].armLowerRightLayers, spriteParts, armLowerRight, color);
        UpdateSpriteControllers(clothes[clothIndex].hipsLayers, spriteParts, hips, color);
        UpdateSpriteControllers(clothes[clothIndex].legLeftLayers, spriteParts, legLeft, color);
        UpdateSpriteControllers(clothes[clothIndex].legLowerLeftLayers, spriteParts, legLowerLeft, color);
        UpdateSpriteControllers(clothes[clothIndex].legRightLayers, spriteParts, legRight, color);
        UpdateSpriteControllers(clothes[clothIndex].legLowerRightLayers, spriteParts, legLowerRight, color);
        UpdateSpriteControllers(clothes[clothIndex].dressRightMiddleLayers, spriteParts, dressRightMiddle, color);
        UpdateSpriteControllers(clothes[clothIndex].dressLeftMiddleLayers, spriteParts, dressLeftMiddle, color);
        UpdateSpriteControllers(clothes[clothIndex].dressRightLayers, spriteParts, dressRight, color);
        UpdateSpriteControllers(clothes[clothIndex].dressLeftLayers, spriteParts, dressLeft, color);
        UpdateSpriteControllers(clothes[clothIndex].dressMiddleLayers, spriteParts, dressMiddle, color);
        UpdateSpriteControllers(clothes[clothIndex].foldedLayers, spriteParts, folded, color);
    }

    internal void CreateArmLayers(string clothPath, Sprite sprite)
    {
        if (_clothLayers.ContainsKey(clothPath) == false)
        {
            _clothLayers.Add(clothPath, new List<ClothCreatorSpritePivotController>());
        }
        var layer = Instantiate(layerPrefab, armLeft.transform);
        layer.SetSprite(sprite);
        layer.gameObject.name = "Outfit_arm_Left_layer_" + _clothLayers[clothPath].Count;
        _clothLayers[clothPath].Add(layer);
    }


    internal void ChangedChannelColor(int clothIndex, int colorChannelIndex, Color newColor)
    {
        var cloth = clothes[clothIndex];
        foreach (var layer in cloth.EveryLayer)
        {
            if (layer == null)
                continue;
            if (layer.Count == 0)
                continue;
            foreach (var controller in layer)
            {
                if (controller.colorChannelIndex == colorChannelIndex)
                {
                    controller.SetColor(newColor);
                }
            }
        }
    }

    internal void ChangedColorChannelIndex(int clothIndex, int layerIndex, int newTintIndex, string name, Color newColor)
    {
        switch (name)
        {
            case ClothNames.TorsoName:
                UpdateControllerColor(clothes[clothIndex].torsoLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.ArmLeftName:
                UpdateControllerColor(clothes[clothIndex].armLeftLayer, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.ArmLeftLowerName:
                UpdateControllerColor(clothes[clothIndex].armLowerLeftLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.ArmRightName:
                UpdateControllerColor(clothes[clothIndex].armRightLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.ArmLowerRightName:
                UpdateControllerColor(clothes[clothIndex].armLowerRightLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.HipsName:
                UpdateControllerColor(clothes[clothIndex].hipsLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.LegLeftName:
                UpdateControllerColor(clothes[clothIndex].legLeftLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.LegLowerLeftName:
                UpdateControllerColor(clothes[clothIndex].legLowerLeftLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.LegRightName:
                UpdateControllerColor(clothes[clothIndex].legRightLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.LegLowerRightName:
                UpdateControllerColor(clothes[clothIndex].legLowerRightLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.DressRightMiddle:
                UpdateControllerColor(clothes[clothIndex].dressRightMiddleLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.DressLeftMiddle:
                UpdateControllerColor(clothes[clothIndex].dressLeftMiddleLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.DressRight:
                UpdateControllerColor(clothes[clothIndex].dressRightLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.DressLeft:
                UpdateControllerColor(clothes[clothIndex].dressLeftLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.DressMiddle:
                UpdateControllerColor(clothes[clothIndex].dressMiddleLayers, layerIndex, newTintIndex, newColor);
                break;
            case ClothNames.FoldedName:
                UpdateControllerColor(clothes[clothIndex].foldedLayers, layerIndex, newTintIndex, newColor);
                break;

        }
    }

    internal bool SaveCloth(int clothIndex, string clothName, bool isShirt, bool isJacket, bool isPants, List<Color> colorChannels)
    {
        clothPrefab.name = clothName;
        clothPrefab.isShirt = isShirt;
        //clothPrefab.shirtHasSleeves = isShirt; OLD code for Outfit gonna need it for the other tool
        clothPrefab.isJacket = isJacket;
        //clothPrefab.jacketHasSleeves = isJacket; OLD code for Outfit gonna need it for the other tool
        clothPrefab.isPants = isPants;
        clothPrefab.tintChannels = colorChannels.ToArray();
        //for (int i = 0; i < colorChannels.Count; i++) OLD code for Outfit gonna need it for the other tool
        //{
        //    clothPrefab.tints[i] = colorChannels[i];
        //}

        var clothController = clothes[clothIndex];

        var setOldChildCount = clothPrefab.spriteSetsParent.childCount;

        for (int i = 0; i < setOldChildCount; i++)
        {
            DestroyImmediate(clothPrefab.spriteSetsParent.GetChild(0).gameObject);
        }

        InstantiateAndSetupLayer(clothController.torsoLayers, ref clothPrefab.torsoSpritePartLayers);
        InstantiateAndSetupLayer(clothController.armRightLayers, ref clothPrefab.armRightSpritePartLayers);
        InstantiateAndSetupLayer(clothController.armLowerRightLayers, ref clothPrefab.armRightLowerSpritePartLayers);
        InstantiateAndSetupLayer(clothController.armLeftLayer, ref clothPrefab.armLeftSpritePartLayers);
        InstantiateAndSetupLayer(clothController.armLowerLeftLayers, ref clothPrefab.armLeftLowerSpritePartLayers);
        InstantiateAndSetupLayer(clothController.hipsLayers, ref clothPrefab.hipsSpritePartLayers);
        InstantiateAndSetupLayer(clothController.legRightLayers, ref clothPrefab.legRightSpritePartLayers);
        InstantiateAndSetupLayer(clothController.legLowerRightLayers, ref clothPrefab.legRightLowerSpritePartLayers);
        InstantiateAndSetupLayer(clothController.legLeftLayers, ref clothPrefab.legLeftSpritePartLayers);
        InstantiateAndSetupLayer(clothController.legLowerLeftLayers, ref clothPrefab.legLeftLowerSpritePartLayers);
        InstantiateAndSetupLayer(clothController.dressRightMiddleLayers, ref clothPrefab.dressRightMiddleLayers);
        InstantiateAndSetupLayer(clothController.dressLeftMiddleLayers, ref clothPrefab.dressLeftMiddleLayers);
        InstantiateAndSetupLayer(clothController.dressRightLayers, ref clothPrefab.dressRightLayers);
        InstantiateAndSetupLayer(clothController.dressLeftLayers, ref clothPrefab.dressLeftLayers);
        InstantiateAndSetupLayer(clothController.dressMiddleLayers, ref clothPrefab.dressMiddleLayers);
        InstantiateAndSetupLayer(clothController.foldedLayers, ref clothPrefab.foldedLayers);
#if UNITY_EDITOR

        var files = Directory.GetFiles(ClothesFolderPath);

        foreach (var file in files)
        {
            var fileName = Path.GetFileNameWithoutExtension(file);
            if (fileName == clothName)
            {
                return false;
            }
        }

        PrefabUtility.SaveAsPrefabAsset(clothPrefab.gameObject, ClothesFolderPath + "/" + clothName + ".prefab");
        return true;
#endif
    }

    private void InstantiateAndSetupLayer(List<ClothCreatorSpritePivotController> pivotControllers, ref OutfitInfromationHolder.ClothPart outfitCLothPart)
    {
        if (pivotControllers.Count == 0)
        {
            outfitCLothPart.spritePartLayers.spriteParts = new List<SpritePartLayers.SpritePart>();
            outfitCLothPart.partRenderer = null;
            return;
        }

        var instantiatedLayer = (GameObject)Instantiate(pivotControllers[0].gameObject, clothPrefab.spriteSetsParent);
        instantiatedLayer.transform.localPosition = Vector3.zero;
        instantiatedLayer.transform.localEulerAngles = Vector3.zero;
        instantiatedLayer.transform.localScale = Vector3.one;
        outfitCLothPart.partRenderer = instantiatedLayer.GetComponent<SpriteRenderer>();

        outfitCLothPart.spritePartLayers.spriteParts = new List<SpritePartLayers.SpritePart>();
        for (int i = 0; i < pivotControllers.Count; i++)
        {
            outfitCLothPart.spritePartLayers.spriteParts.Add(
                new SpritePartLayers.SpritePart
                {
                    sprite = pivotControllers[i].CurrentSprite,
                    tintChannel = pivotControllers[i].colorChannelIndex
                });
        }

        DestroyImmediate(instantiatedLayer.transform.GetComponent<ClothCreatorSpritePivotController>());
        foreach (Transform child in instantiatedLayer.transform)
        {
            DestroyImmediate(child.GetComponent<ClothCreatorSpritePivotController>());
        }


    }

    private void UpdateControllerColor(List<ClothCreatorSpritePivotController> spriteControllers, int spriteIndex, int newTintIndex, Color newColor)
    {
        spriteControllers[spriteIndex].colorChannelIndex = newTintIndex;
        spriteControllers[spriteIndex].SetColor(newColor);
    }
}
#endif
