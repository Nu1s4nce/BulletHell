public interface ICardsGenerator
{
    public CardType GetTypeOfCardToGenerate();
    CardsRarenessColors GetColorByRareness(RarenessOfCard rarenessOfCard);
    CardsRarenessColors GetUniqueCardColor();
    public NormalCardConfig GenerateNormalCard();
    public UniqueCardConfig GenerateUniqueCard(RarenessOfCard rarenessOfCard);
}