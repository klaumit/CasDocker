using System.Windows.Forms;

namespace MemForge
{
	internal sealed class NoteContext : ApplicationContext
	{
		private readonly NoteIcon _obj;

		public NoteContext()
		{
			_obj = new NoteIcon();
			_obj.noteIcon.Visible = true;
		}

		protected override void Dispose(bool disposing)
		{
			_obj.noteIcon.Dispose();
			base.Dispose(disposing);
		}
	}
}