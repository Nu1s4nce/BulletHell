public interface ICardsGenerator
{
    public CardType GetTypeOfCardToGenerate();
    CardsRarenessColors GetColorByRareness(RarenessOfCard rarenessOfCard);
    public RarenessOfCard GetRandomRarenessOfCard();
    public NormalCardConfig GenerateNormalCard(RarenessOfCard rarenessOfCard);
    public UniqueCardConfig GenerateUniqueCard(RarenessOfCard rarenessOfCard);
}