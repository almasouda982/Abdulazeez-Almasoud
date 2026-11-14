import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../../providers/CourseBookProvider.dart';
import '../../providers/BookProvider.dart';
import '../../data/connection.dart';
import 'detailScreens/courseBookDetailsScreen.dart';

class CourseBookScreen extends StatefulWidget {
  final int courseId;
  final String courseTitle;

  const CourseBookScreen({required this.courseId, required this.courseTitle, super.key});

  @override
  State<CourseBookScreen> createState() => _CourseBookScreenState();
}

class _CourseBookScreenState extends State<CourseBookScreen> {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      context.read<CourseBookProvider>().fetchBooksForCourse(widget.courseId);
      context.read<BookProvider>().fetchBooks();
    });
  }

  Future<void> deleteBook(int bookId) async {
    final success = await context.read<CourseBookProvider>().removeBookFromCourse(widget.courseId, bookId);

    if (!mounted) return;

    if (success) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Book successfully removed from course')),
      );
    } else {
      final err = context.read<CourseBookProvider>().error ?? 'Error removing book';
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text(err)),
      );
    }
  }

  void _showAddBookDialog(List<dynamic> currentAssignedBooks) {
    final bookProvider = context.read<BookProvider>();
    final allBooks = bookProvider.books;

    final assignedBookIds = currentAssignedBooks.map((cb) {
      final bData = cb['book'] ?? cb['Book'] ?? {};
      return cb['bookId'] ?? cb['BookId'] ?? bData['id'] ?? bData['Id'];
    }).toSet();

    final availableBooks = allBooks.where((book) {
      final bookId = book['id'] ?? book['Id'];
      return !assignedBookIds.contains(bookId);
    }).toList();

    showDialog(
      context: context,
      builder: (context) {
        return AlertDialog(
          shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(20)),
          title: const Text(
            'Assign Book to Course',
            style: TextStyle(fontWeight: FontWeight.bold, fontSize: 18),
          ),
          content: SizedBox(
            width: double.maxFinite,
            height: 350,
            child: availableBooks.isEmpty
                ? const Center(
              child: Text(
                'All available books are already assigned to this course.',
                textAlign: TextAlign.center,
                style: TextStyle(color: Colors.grey),
              ),
            )
                : ListView.builder(
              itemCount: availableBooks.length,
              itemBuilder: (context, index) {
                final book = availableBooks[index];
                final bookId = book['id'] ?? book['Id'];
                final bookTitle = book['title'] ?? book['Title'] ?? 'Unknown Book';
                final bookAuthor = book['author'] ?? book['Author'] ?? 'N/A';

                return Container(
                  margin: const EdgeInsets.symmetric(vertical: 4),
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(12),
                    border: Border.all(color: Colors.grey.shade200),
                  ),
                  child: ListTile(
                    title: Text(
                      bookTitle,
                      style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 14),
                    ),
                    subtitle: Text('Author: $bookAuthor', style: TextStyle(fontSize: 12, color: Colors.grey[600])),
                    trailing: ElevatedButton(
                      style: ElevatedButton.styleFrom(
                        backgroundColor: Colors.indigo,
                        foregroundColor: Colors.white,
                        elevation: 0,
                        shape: RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(8),
                        ),
                      ),
                      onPressed: () async {
                        final success = await context.read<CourseBookProvider>().addBookToCourse(widget.courseId, bookId);

                        if (!mounted) return;
                        Navigator.pop(context);

                        if (success) {
                          ScaffoldMessenger.of(context).showSnackBar(
                            const SnackBar(content: Text('Book successfully added to course')),
                          );
                        } else {
                          final err = context.read<CourseBookProvider>().error ?? 'Error adding book';
                          ScaffoldMessenger.of(context).showSnackBar(
                            SnackBar(content: Text(err)),
                          );
                        }
                      },
                      child: const Text('Add', style: TextStyle(fontSize: 12)),
                    ),
                  ),
                );
              },
            ),
          ),
          actions: [
            TextButton(
              onPressed: () => Navigator.pop(context),
              child: const Text('Cancel', style: TextStyle(color: Colors.grey)),
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.grey[50],
      appBar: AppBar(
        title: Text(
          '${widget.courseTitle} - Books',
          style: const TextStyle(fontWeight: FontWeight.bold, color: Colors.white),
        ),
        backgroundColor: Colors.indigo,
        elevation: 0,
        centerTitle: true,
        iconTheme: const IconThemeData(color: Colors.white),
      ),
      body: Consumer<CourseBookProvider>(
        builder: (context, courseBookProvider, child) {
          if (courseBookProvider.isLoading) {
            return const Center(
              child: CircularProgressIndicator(color: Colors.indigo),
            );
          }

          if (courseBookProvider.error != null) {
            return Center(
              child: Text(
                'Error: ${courseBookProvider.error}',
                style: const TextStyle(color: Colors.redAccent),
              ),
            );
          }

          final booksList = courseBookProvider.courseBooks;

          return Scaffold(
            backgroundColor: Colors.grey[50],
            floatingActionButton: FloatingActionButton(
              onPressed: () => _showAddBookDialog(booksList),
              backgroundColor: Colors.indigo,
              child: const Icon(Icons.add, color: Colors.white),
            ),
            body: booksList.isEmpty
                ? Center(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Icon(Icons.menu_book_rounded, size: 64, color: Colors.grey[400]),
                  const SizedBox(height: 16),
                  Text(
                    'No books assigned to this course yet.\nTap + to assign books.',
                    textAlign: TextAlign.center,
                    style: TextStyle(color: Colors.grey[600], fontSize: 14, height: 1.4),
                  ),
                ],
              ),
            )
                : Column(
              children: [
                Expanded(
                  child: ListView.builder(
                    padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
                    itemCount: booksList.length,
                    itemBuilder: (context, index) {
                      final courseBook = booksList[index];
                      final bookData = courseBook['book'] ?? courseBook['Book'] ?? {};

                      final title = bookData['title'] ?? bookData['Title'] ?? 'N/A';
                      final author = bookData['author'] ?? bookData['Author'] ?? 'N/A';
                      final price = bookData['price'] ?? bookData['Price'] ?? 0.00;

                      final bookId = courseBook['bookId'] ?? courseBook['BookId'] ?? bookData['id'] ?? bookData['Id'];

                      final String imagePath = bookData['imageUrl']?.toString() ?? bookData['ImageUrl']?.toString() ?? '';
                      final String imageUrl = imagePath.isEmpty
                          ? ''
                          : Uri.parse(ConnectionString.dashboard)
                          .resolve(imagePath)
                          .toString();

                      return Container(
                        margin: const EdgeInsets.symmetric(vertical: 6),
                        decoration: BoxDecoration(
                          color: Colors.white,
                          borderRadius: BorderRadius.circular(16),
                          border: Border.all(color: Colors.grey.shade200),
                          boxShadow: [
                            BoxShadow(
                              color: Colors.black.withOpacity(0.02),
                              blurRadius: 8,
                              offset: const Offset(0, 3),
                            ),
                          ],
                        ),
                        child: Material(
                          color: Colors.transparent,
                          borderRadius: BorderRadius.circular(16),
                          child: InkWell(
                            borderRadius: BorderRadius.circular(16),
                            onTap: () {
                              Navigator.push(
                                context,
                                MaterialPageRoute(
                                  builder: (context) => CourseBookDetailsScreen(bookData),
                                ),
                              );
                            },
                            child: Padding(
                              padding: const EdgeInsets.all(12),
                              child: Row(
                                children: [
                                  ClipRRect(
                                    borderRadius: BorderRadius.circular(10),
                                    child: imageUrl.isNotEmpty
                                        ? Image.network(
                                      imageUrl,
                                      width: 50,
                                      height: 70,
                                      fit: BoxFit.cover,
                                      errorBuilder: (context, error, stackTrace) {
                                        return Container(
                                          width: 50,
                                          height: 70,
                                          color: Colors.indigo.withOpacity(0.08),
                                          child: const Icon(Icons.book, size: 24, color: Colors.indigo),
                                        );
                                      },
                                    )
                                        : Container(
                                      width: 50,
                                      height: 70,
                                      color: Colors.indigo.withOpacity(0.08),
                                      child: const Icon(Icons.book, size: 24, color: Colors.indigo),
                                    ),
                                  ),
                                  const SizedBox(width: 16),
                                  Expanded(
                                    child: Column(
                                      crossAxisAlignment: CrossAxisAlignment.start,
                                      children: [
                                        Text(
                                          title.toString(),
                                          style: const TextStyle(
                                            fontWeight: FontWeight.bold,
                                            fontSize: 15,
                                            color: Colors.black87,
                                          ),
                                        ),
                                        const SizedBox(height: 4),
                                        Text(
                                          'Author: $author',
                                          style: TextStyle(fontSize: 13, color: Colors.grey[600]),
                                        ),
                                        const SizedBox(height: 6),
                                        Text(
                                          '\$$price',
                                          style: const TextStyle(
                                            fontSize: 14,
                                            color: Colors.green,
                                            fontWeight: FontWeight.bold,
                                          ),
                                        ),
                                      ],
                                    ),
                                  ),
                                  IconButton(
                                    icon: const Icon(Icons.delete_outline_rounded, color: Colors.redAccent),
                                    onPressed: () {
                                      if (bookId != null) {
                                        deleteBook(bookId);
                                      }
                                    },
                                  ),
                                ],
                              ),
                            ),
                          ),
                        ),
                      );
                    },
                  ),
                ),
                Container(
                  padding: const EdgeInsets.all(16),
                  width: double.infinity,
                  decoration: BoxDecoration(
                    color: Colors.white,
                    border: Border(top: BorderSide(color: Colors.grey.shade200)),
                  ),
                  child: Text(
                    'Total Books Cost: \$${booksList.fold<double>(0.0, (sum, cb) {
                      final bData = cb['book'] ?? cb['Book'] ?? {};
                      return sum + ((bData['price'] ?? bData['Price'] ?? 0) as num).toDouble();
                    }).toStringAsFixed(2)}',
                    textAlign: TextAlign.center,
                    style: const TextStyle(
                      fontSize: 18,
                      fontWeight: FontWeight.bold,
                      color: Colors.green,
                    ),
                  ),
                ),
              ],
            ),
          );
        },
      ),
    );
  }
}