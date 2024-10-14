public interface ICardsGenerator
{
    public CardType GetTypeOfCardToGenerate();
    public NormalCardConfig GenerateNormalCard();
    public UniqueCardConfig GenerateUniqueCard();
}