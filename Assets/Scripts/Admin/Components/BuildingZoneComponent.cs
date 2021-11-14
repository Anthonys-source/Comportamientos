using System;
using System.Collections.Generic;

[Serializable]
public class BuildingZoneComponent
{
    public ID m_ID;
    public Action<CharacterEntity> OnCharacterEnter;
    public List<ID> m_CharactersInside;
}
