using System.Windows.Forms;

namespace DevForge.UI.Tools
{
	public static class GuiExt
	{
		public static T FindParent<T>(this Control ctrl) where T : class
		{
			if (ctrl == null)
				return null;

			var item = ctrl as T;
			if (item != null)
				return item;

			return FindParent<T>(ctrl.Parent);
		}
	}
}