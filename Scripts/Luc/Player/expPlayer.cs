using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class expPlayer : MonoBehaviour
{
   
   public playerData _playerData;

    public void upExp()
    {
        _playerData.expValue +=2;
        if (_playerData.expValue == _playerData.expMax)
        {
           _playerData.levelValue += 1;
            _playerData.expMax += 10;
            _playerData.expValue = 0;
        }    
    }

}
