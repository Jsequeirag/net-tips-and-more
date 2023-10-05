// See https://aka.ms/new-console-template for more information
Console.WriteLine("print in new line");
Console.WriteLine("print inasdasd");


char userOption='o';

int gameScore=10;

decimal decimalValue=1.34023m;

double doubleValue = 1000;

float floatValue = 1.34023f;

bool booleanValue=false;

var explicitStringValue = "String";

var explicitBooleanValue=false;

//siempre hay que asignar el valor a una variable explicita para imprimir
Console.WriteLine(booleanValue);

/**Trabajar con cadenas**/
Console.WriteLine("Generating invoices for customer \"Contoso Corp\" ...\n");
Console.WriteLine("Invoice: 1021\t\tComplete!");
Console.WriteLine("Invoice: 1022\t\tComplete!");
Console.WriteLine("\nOutput Directory:\t");
/*contatenar cadenas*/
string firstName = "Bob";
string greeting = "Hello";
string message = greeting + " " + firstName + "!";
/*interpolacion de cadenas */
string newMessage = $"{greeting} {firstName}!";

/**sumatoria de números**/
 int value = 1;

value = value + 1;
Console.WriteLine("First increment: " + value);

value += 1;
Console.WriteLine("Second increment: " + value);

value++;
Console.WriteLine("Third increment: " + value);

value = value - 1;
Console.WriteLine("First decrement: " + value);

value -= 1;
Console.WriteLine("Second decrement: " + value);

value--;
Console.WriteLine("Third decrement: " + value);

int value1 = 1;
value1++;
Console.WriteLine("First: " + value1);
Console.WriteLine($"Second: {value1++}");//en c# se hace la operacion pero no se muestra hasta la siguiente linea 
Console.WriteLine("Third: " + value1);
Console.WriteLine("Fourth: " + (++value1));

/***Proyecto calculo de notas****/
string studentName = "Sophia Johnson";
string course1Name = "English 101";
string course2Name = "Algebra 101";
string course3Name = "Biology 101";
string course4Name = "Computer Science I";
string course5Name = "Psychology 101";

int course1Credit = 3;
int course2Credit = 3;
int course3Credit = 4;
int course4Credit = 4;
int course5Credit = 3;

int gradeA = 4;
int gradeB = 3;

int course1Grade = gradeA;
int course2Grade = gradeB;
int course3Grade = gradeB;
int course4Grade = gradeB;
int course5Grade = gradeA;

int totalCreditHours = 0;
totalCreditHours += course1Credit;
totalCreditHours += course2Credit;
totalCreditHours += course3Credit;
totalCreditHours += course4Credit;
totalCreditHours += course5Credit;

int totalGradePoints = 0;
totalGradePoints += course1Credit * course1Grade;
totalGradePoints += course2Credit * course2Grade;
totalGradePoints += course3Credit * course3Grade;
totalGradePoints += course4Credit * course4Grade;
totalGradePoints += course5Credit * course5Grade;

decimal gradePointAverage = (decimal)totalGradePoints / totalCreditHours;

int leadingDigit = (int)gradePointAverage;
int trailingDigits = (int)(gradePointAverage * 100) - (leadingDigit * 100);

Console.WriteLine($"Student: {studentName}\n");
Console.WriteLine("Course\t\t\t\tGrade\tCredit Hours");

Console.WriteLine($"{course1Name}\t\t\t{course1Grade}\t\t{course1Credit}");
Console.WriteLine($"{course2Name}\t\t\t{course2Grade}\t\t{course2Credit}");
Console.WriteLine($"{course3Name}\t\t\t{course3Grade}\t\t{course3Credit}");
Console.WriteLine($"{course4Name}\t{course4Grade}\t\t{course4Credit}");
Console.WriteLine($"{course5Name}\t\t{course5Grade}\t\t{course5Credit}");

Console.WriteLine($"\nFinal GPA:\t\t\t{leadingDigit}.{trailingDigits}");