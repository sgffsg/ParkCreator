using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public SceneryManager sceneryManager;                                                           //Менеджер объектов
    public static ShopManager instance;                                                             //Ссылка на скрипт


    [SerializeField] GameObject ShopPage;                                                           //Страница магазина
    [SerializeField] Text PageName;                                                                 //Компонент [Text] для отображения названия страницы магазина
    [SerializeField] Scrollbar scrollbar;                                                           //Компонент [ScrollBar] для Списка Покупок


    public GameObject prefab;                                                                       //Префаб Элемента магазина
    public GameObject content;                                                                      //Префаб Контента для размения Квестов


    private List<GameObject> ShopList = new List<GameObject>();                                     //Список объектов для покупки
    private string DebugStr = null;                                                         //Переменная для Логов




    private void Start() 
    {
        instance = this;
    }

    public void InitializeMenu()
    {
        this.gameObject.SetActive(true);
        ShopPage.SetActive(false);
    }

    public void Close(string mode)
    {
        if (mode == "ClosePage")
            ShopPage.SetActive(false);
        if (mode == "CloseMenu")
            this.gameObject.SetActive(false);
    }

    private void ResetPages()
    {
        for(int i = 0; i < ShopList.Count; i++)
        {
            Destroy(ShopList[i]);
        }
        ShopList.Clear();
        scrollbar.value = 0;
    }

    public void InitializeShopPage(string pageName)
    {
        DebugStr = $"Shop[{pageName}]: Open\n";
        DEBUGGER.Log(ColorType.Yellow, DebugStr);
        ShopPage.SetActive(true);
        ResetPages();
        switch(pageName)
        {
            case "Donate":
            PageName.text = "Магазин [Валюта]";
            for (int i = 0; i < sceneryManager.DonateItems.Length; i++)
            {
                InstantItem(sceneryManager.DonateItems[i]);
            }
            break;
            case "Attractions":
            PageName.text = "Магазин [Аттракционы]";
            for (int i = 0; i < sceneryManager.AttractionsItems.Length; i++)
            {
                InstantItem(sceneryManager.AttractionsItems[i]);
            }
            break;
            case "Flora":
            PageName.text = "Магазин [Природа]";
            for (int i = 0; i < sceneryManager.FloraItems.Length; i++)
            {
                InstantItem(sceneryManager.FloraItems[i]);
            }
            break;
            case "Obstacles":
            PageName.text = "Магазин [Ограждения]";
            for (int i = 0; i < sceneryManager.ObstaclesItems.Length; i++)
            {
                InstantItem(sceneryManager.ObstaclesItems[i]);
            }
            break;
            case "Buildings":
            PageName.text = "Магазин [Постройки]";
            for (int i = 0; i < sceneryManager.BuildingsItems.Length; i++)
            {
                InstantItem(sceneryManager.BuildingsItems[i]);
            }
            break;
            case "Decor":
            PageName.text = "Магазин [Украшения]";
            for (int i = 0; i < sceneryManager.DecorItems.Length; i++)
            {
                InstantItem(sceneryManager.DecorItems[i]);
            }
            break;
            case "Other":
            PageName.text = "Магазин [Разное]";
            for (int i = 0; i < sceneryManager.OtherItems.Length; i++)
            {
                InstantItem(sceneryManager.OtherItems[i]);
            }
            break;
        }
        DebugStr = $"ShopPageItems: IsLoaded\n";
        DEBUGGER.Log(ColorType.Purple, DebugStr);
        UPD();
    }
    
    public void UPD()
    {
        if (scrollbar.value < 0)
            scrollbar.value = 0;
        if (scrollbar.value > 1)
            scrollbar.value = 1;

    }
    private void InstantItem(Scenery scenery)
    {
        //Фиолетовый   [#532DC3]
        //Зеленый       [#247631]
        //Синий         [#2590E0]
        GameObject shopItem = Instantiate(prefab);
        shopItem.name = "ShopItem#"+scenery.nameScenery;
        shopItem.transform.SetParent(content.transform, false);
        shopItem.transform.Find("Name").GetComponent<Text>().text = scenery.nameScenery;
        shopItem.transform.Find("Icon").GetComponent<Image>().sprite = scenery.sceneryIcon;
        shopItem.GetComponent<ShopItem>().scenery = scenery;
        shopItem.GetComponent<ShopItem>().shopManager = this.gameObject.GetComponent<ShopManager>();
        
        shopItem.transform.Find("Annotation").transform.Find("Text").GetComponent<Text>().fontSize = 23;
        shopItem.transform.Find("Annotation").gameObject.SetActive(true);

        if (scenery.IsRecomended)
        {
            //Зеленый
            shopItem.transform.Find("BuyButton").GetComponent<Image>().color = new Color32(36, 119, 49, 248);
            shopItem.transform.Find("Annotation").transform.Find("Image").GetComponent<Image>().color = new Color32(36, 119, 49, 248);
            shopItem.transform.Find("Annotation").transform.Find("Text").GetComponent<Text>().text = "Рекомендуем";
        }
        else
        {
            if (scenery.IsLimited)
            {
                //Фиолетовый
                shopItem.gameObject.transform.Find("BuyButton").GetComponent<Image>().color = new Color32(83, 45, 195, 248);
                shopItem.transform.Find("Annotation").transform.Find("Image").GetComponent<Image>().color = new Color32(83, 45, 195, 248);
                shopItem.transform.Find("Annotation").transform.Find("Text").GetComponent<Text>().text = "Ограниченное время!\n2д 12ч 39м 2с";
                shopItem.transform.Find("Annotation").transform.Find("Text").GetComponent<Text>().fontSize = 15;
            }
            else
            {
                if (scenery.IsNewObject)
                {
                    //Синий
                    shopItem.gameObject.transform.Find("BuyButton").GetComponent<Image>().color = new Color32(37, 144, 224, 248);
                    shopItem.transform.Find("Annotation").transform.Find("Image").GetComponent<Image>().color = new Color32(37, 144, 224, 248);
                    shopItem.transform.Find("Annotation").transform.Find("Text").GetComponent<Text>().text = "Новое";
                }
                else
                {
                    //Зеленый
                    shopItem.gameObject.transform.Find("BuyButton").GetComponent<Image>().color = new Color32(36, 119, 49, 248);
                    shopItem.transform.Find("Annotation").transform.Find("Image").GetComponent<Image>().color = new Color32(36, 119, 49, 248);
                    shopItem.transform.Find("Annotation").transform.Find("Text").GetComponent<Text>().text = "";
                    shopItem.transform.Find("Annotation").gameObject.SetActive(false);
                }
            }
        }
        shopItem.transform.Find("BuyButton").gameObject.transform.Find("CostText").GetComponent<Text>().text = "Выбрать";
        ShopList.Add(shopItem);
    }
}
