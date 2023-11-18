using System;
using System.Collections;
using System.Collections.Generic;

public class Book : ICloneable
{
    public string Title { get; set; }
    public string Author { get; set; }

    public object Clone()
    {
        return new Book { Title = this.Title, Author = this.Author };
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Book otherBook = (Book)obj;
        return Title == otherBook.Title && Author == otherBook.Author;
    }

    public override int GetHashCode()
    {
        return Tuple.Create(Title, Author).GetHashCode();
    }

    public override string ToString()
    {
        return $"{Title} by {Author}";
    }
}

public class BookList : IEnumerable<Book>, ICloneable
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public void RemoveBook(Book book)
    {
        books.Remove(book);
    }

    public bool ContainsBook(Book book)
    {
        return books.Contains(book);
    }

    public IEnumerator<Book> GetEnumerator()
    {
        return books.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public object Clone()
    {
        BookList clone = new BookList();
        foreach (var book in books)
        {
            clone.AddBook((Book)book.Clone());
        }
        return clone;
    }
}

class Program
{
    static void Main()
    {
        BookList bookList = new BookList();

        // Добавление книг
        bookList.AddBook(new Book { Title = "Book1", Author = "Author1" });
        bookList.AddBook(new Book { Title = "Book2", Author = "Author2" });

        // Вывод списка книг
        Console.WriteLine("Books in the list:");
        foreach (var book in bookList)
        {
            Console.WriteLine(book);
        }

        // Проверка наличия книги в списке
        Book bookToCheck = new Book { Title = "Book1", Author = "Author1" };
        Console.WriteLine($"Is {bookToCheck} in the list? {bookList.ContainsBook(bookToCheck)}");

        // Удаление книги
        bookList.RemoveBook(bookToCheck);

        // Вывод обновленного списка книг
        Console.WriteLine("\nBooks in the list after removal:");
        foreach (var book in bookList)
        {
            Console.WriteLine(book);
        }

        // Клонирование списка книг
        BookList clonedList = (BookList)bookList.Clone();

        // Вывод клонированного списка книг
        Console.WriteLine("\nCloned list:");
        foreach (var book in clonedList)
        {
            Console.WriteLine(book);
        }
    }
}
