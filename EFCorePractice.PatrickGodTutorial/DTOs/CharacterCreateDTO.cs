namespace EFCorePractice.PatrickGodTutorial.DTOs
{
    public record struct CharacterCreateDTO(string Name, 
                                            BackpackCreateDTO Backpack, 
                                            List<WeaponCreateDTO> Weapons,
                                            List<TeamCreateDTO> Teams
                                            );
}
