using UnityEngine;
using System;

public enum Place
{
    InHouse,
    InBuilding,
    InOutside
}

public enum Status
{ 
  Stay,
  Move,
  Action
}

public class Character : MonoBehaviour
{
    private House _nowHouse;
    private Place _place = Place.InOutside;

    public static Action<Place> OnSetPlace;
    public static Action<House> OnSetHouse;

    private void OnEnable()
    {
        OnSetPlace += SetPlace;
        OnSetHouse += SetHouse;
    }

    private void OnDisable()
    {
        OnSetPlace -= SetPlace;
        OnSetHouse -= SetHouse;
    }

    private void SetHouse(House house)
    {
        _nowHouse = house;
    }

    private void SetPlace(Place place)
    {
        _place = place;
    }

    public Place GetPlace()
    {
        return _place;
    }

    public House GetHouse()
    {
        return _nowHouse;
    }
}
