using IGraphics.Mathematics;

namespace RetroGraph.Models.Extensions
{
    public static class CardanFrameExtension
    {
        public static CardanFrameDto ToCardanFrameDto(this CardanFrame cardanFrame)
        {
            var cardanFrameDto = new CardanFrameDto()
            {
                OffsetX = cardanFrame.Offset.X,
                OffsetY = cardanFrame.Offset.Y,
                OffsetZ = cardanFrame.Offset.Z,
                AlphaAngleAxisX = cardanFrame.AlphaAngleAxisX,
                BetaAngleAxisY = cardanFrame.BetaAngleAxisY,
                GammaAngleAxisZ = cardanFrame.GammaAngleAxisZ
            };

            return cardanFrameDto;
        }
    }
}
