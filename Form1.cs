namespace SQLite
{
    public partial class Form1 : Form
    {
        private DatabaseManager databaseManager = new DatabaseManager();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadPeople();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var person = new Person
            {
                Name = textBox1.Text,
                Surname = textBox2.Text,
                Age = (int) numericUpDown1.Value
            };
            databaseManager.AddPerson(person);
            loadPeople();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void loadPeople()
        {
            listBox1.Items.Clear();

            var people = databaseManager.GetPeople();

            foreach (var person in people)
            {
                listBox1.Items.Add($"{person.Name} {person.Surname}, wiek: {person.Age}");
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            databaseManager.DeletePerson();
            loadPeople();
        }
    }
}