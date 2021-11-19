using System.Windows.Forms; 

namespace Lab_I_2
{
    public class Model
    {
        private int dataForMdl;
        public int AccessCheck;
        public Model() { this.dataForMdl = 22; }
        public void op()
        {
            MessageBox.Show("C o o l  b u s i n e s s  l o g i c");
        }
        public int mdl()
        {
            // Метод модели
            return this.dataForMdl;
        }
    }
}
