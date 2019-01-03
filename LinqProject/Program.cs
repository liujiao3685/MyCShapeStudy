using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqProject
{
    class Program
    {
        // Create a data source by using a collection initializer. 
        public static List<Student> ListStudents = new List<Student>
        {
           new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}},
           new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
           new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {88, 94, 65, 91}},
           new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {97, 89, 85, 82}},
           new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}},
           new Student {First="Fadi", Last="Fakhouri", ID=116, Scores= new List<int> {99, 86, 90, 94}},
           new Student {First="Hanying", Last="Feng", ID=117, Scores= new List<int> {93, 92, 80, 87}},
           new Student {First="Hugo", Last="Garcia", ID=118, Scores= new List<int> {92, 90, 83, 78}},
           new Student {First="Lance", Last="Tucker", ID=119, Scores= new List<int> {68, 79, 88, 92}},
           new Student {First="Terry", Last="Adams", ID=120, Scores= new List<int> {99, 82, 81, 79}},
           new Student {First="Eugene", Last="Zabokritski", ID=121, Scores= new List<int> {96, 85, 91, 60}},
           new Student {First="Michael", Last="Tucker", ID=122, Scores= new List<int> {94, 92, 91, 91} }
        };

        static void Main(string[] args)
        {

            SelectAverage();

            SelectByGroup();

            Select();

            Console.ReadKey();
        }

        private static void SelectAverage()
        {
            //计算班级学生的总分
            var studentQuery =
                from student in ListStudents
                let totalScore = student.Scores[0] + student.Scores[1] + student.Scores[2] + student.Scores[3]
                select totalScore;

            //计算平均分
            double averageScore = studentQuery.Average();
            Console.WriteLine("Class Average Score is " + averageScore);
        }

        private static void SelectByGroup()
        {
            //按姓氏的首字母 分组
            var studentQuery =
                from student in ListStudents
                group student by student.First[0];

            foreach (var item in studentQuery)
            {
                Console.WriteLine(item.Key);
                foreach (var s in item)
                {
                    Console.WriteLine("{0},{1}", s.First, s.Last);
                }
            }
        }

        private static void Select()
        {
            //查询所有学生在最后一次考试成绩大于80的学生 ,并且第一次成绩大于90
            IEnumerable<Student> students =
                from student in ListStudents
                where student.Scores[student.Scores.Count - 1] > 80 && student.Scores[0] > 90
                orderby student.First descending
                //根据姓名排序 ascending顺序 descending逆序
                select student;

            //打印学生姓名
            foreach (Student student in students)
            {
                Console.WriteLine(student.First);
            }
        }
    }

    public class Student
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int ID { get; set; }
        public List<int> Scores;
    }
}
