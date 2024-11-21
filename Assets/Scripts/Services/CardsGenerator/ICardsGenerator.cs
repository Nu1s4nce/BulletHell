public interface ICardsGenerator
{
    public CardType GetTypeOfCardToGenerate();
    CardsRarenessColors GetColorByRareness(RarenessOfCard rarenessOfCard);
    public NormalCardConfig GenerateNormalCard();
    public UniqueCardConfig GenerateUniqueCard(RarenessOfCard rarenessOfCard);
}