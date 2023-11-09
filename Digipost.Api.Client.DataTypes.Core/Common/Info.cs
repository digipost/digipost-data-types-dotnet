namespace Digipost.Api.Client.DataTypes.Core.Common
{
    public class Info
    {
        public string Title { get; }
        public string Text { get; }

        public Info(string title, string text)
        {
            Title = title;
            Text = text;
        }
    }

    internal static class InfoToDto
    {
        internal static Internal.Info ToDto(this Info info)
        {
            return new Internal.Info
            {
                Title = info.Title,
                Text = info.Text
            };
        }
    }
}
