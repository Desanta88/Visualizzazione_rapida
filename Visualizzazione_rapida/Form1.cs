using System.Text;
namespace Visualizzazione_rapida
{
    public partial class Form1 : Form
    {
        public string filename = @"./veneto_verona.csv";
        public string line="",nc="",ne="",edificio="";
        public int nr,posi;
        //creare una funziona che conta il numero di byte totale così che poi se faccio la ricerca e mi si posiziona a metà del file allora se divido il numero totale di byte e divido per pos,allora ottengo la riga
        //creare una funzione che attraverso la ricerca dicotomica,posiziona il puntatore nella i a metà e di conseguenza,se non è quello spostati indietro o avanti
        public int RicercaDicotomica( ref byte[] b,int r)
        {
            int pos = -1, m = 0, i = 0,j=r, sc = 0 ;
            string line="",co="";
            m = r / 2;
            do
            {
                var f = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryReader n = new BinaryReader(f);
                f.Seek((m * 528)-528, SeekOrigin.Current);
                b = n.ReadBytes(528);
                line = Encoding.ASCII.GetString(b, 0, b.Length);
                co=FromStringComune(line, ";");
                sc = string.Compare(co, textBox1.Text);
                if (co == textBox1.Text)
                {
                    pos = m;
                }
                else if (sc < 0)
                {
                    i = m++;
                }
                else if(sc>0)
                {
                    j = m--;
                }
            } while ((m>=0 && m<=r) && pos == -1);

            return pos;

        }
        public int contaByte(byte[] b)
        {
            int fileLenght = 528;
            var f = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(f);
            f.Seek(0, SeekOrigin.Begin);
            for(int i = 0; i < 2592; i++)
            {
                b = r.ReadBytes(fileLenght);
                fileLenght = fileLenght+528;
            }
           return fileLenght;
        }
        public int contaPos(byte[] b)
        {
            int lineTOT;
            int lineLenght = 528;
            var f = new FileStream(filename, FileMode.Open, FileAccess.Read);
            int lineBytes = Convert.ToInt32(f.Length);
            BinaryReader r = new BinaryReader(f);
            f.Seek(0, SeekOrigin.Begin);
            lineTOT = lineBytes / lineLenght;
            return lineTOT;

        }

        public string FromStringComune(string s,string sep = ";")
        {
            string co;
            string[] record=s.Split(sep);
            co = record[0];
            return co;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public string FromStringEdi(string s, string sep = ";")
        {
            string co;
            string[] record = s.Split(sep);
            co = record[7];
            return co;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*int recordLenght = 528;
            var f = new FileStream(filename, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(f);
            byte[] br;
            f.Seek(0, SeekOrigin.Begin);
            br =r.ReadBytes(recordLenght);
            line = Encoding.ASCII.GetString(br, 0, br.Length);
            nc=FromStringComune(line, ";");
            textBox1.Text = nc;
            ne = FromStringEdi(line, ";");
            textBox2.Text = ne;*/
            
            byte[] br = {0};
            //fl=contaByte(br);
            nr = contaPos(br)+1;
            posi = RicercaDicotomica(ref br, nr);
            line = Encoding.ASCII.GetString(br, 0, br.Length);
            edificio = FromStringEdi(line, ";");
            label2.Visible = true;
            label3.Visible = true;
            label3.Text = edificio;
            //textBox1.Text = posi.ToString();


        }
    }
}