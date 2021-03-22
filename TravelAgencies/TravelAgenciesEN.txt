Files without comment:
//  I certify that this assignment is entirely my own work, performed independently and without any help from the sources which are not allowed.
//  <name> <surname>
won't be considered as part of solution(must be added in Program, no need to be added anywhere in Init)


Travel Agencies

The task is to implement a program to view websites with offers from three fake travel agencies (Travel Agency). These agencies do not have a reliable offer, but they are based on data from websites such as Booking.com or TripAdvisor.com. In addition, they insert photos that do not have much in common with reality, as well as fake opinions/reviews about their offers.


---DATABASE---

Each travel agency uses the same databases. Data should be read in the given order. Available databases:
	-BookingDatabase: stores rental rooms divided into categories unknown to us. Each category is a one-way linked list, and each node stores the name of the offer, price and rating. Take one room at a time from the following categories (first the first room from the first category, then the first from the second category, ... then the first from the n-th category, then the second from the first category ..)
	-OysterDatabase: stores trip reviews with their authors in a tree that you should traverse like it was a BST tree (left subtree -> current node -> right subtree)
	-ShutterStockDatabase: stores photos unknown to us divided into categories/groups/albums and their nesting up to max 3 levels. Each group, subgroup or subgroup may be empty or even be null. Whole sets of metadata are stored, which can be useful later.
	-TripAdvisorDatabase: stores trip data in a rather strange way. Trip IDs are kept separate (should be used in order). Prices, ratings and countries are in the respective dictionaries. Trip names are divided into unknown categories and the searched name (corresponding to the given id) is in one of the dictionaries. It may turn out that the data is incomplete and any data is missing, in which case you should skip a given trip and immediately go to the next one.

Data for the above databases were generated in the Run method.

Requirements:
	-Each of travel agencies goes through the database in a given order independently, i.e. readings from a given database by one travel agency have no relation / influence with readings by other travel agencies.
	-If the travel agency goes through all the data, it starts browsing from the beginning.
	-Data cannot be stored. They are loaded individually only as needed.
	-An additional condition is that all databases should give the same interface/interfaces (can be parameterized differently).


---ENCRYPTION---

The data provided is partially encrypted. Numbers (int) are encrypted, but the int values ​​are encoded as strings with all digits retained so that they can be decoded correctly. Coded fields are marked with a comment. Each database has a different cipher, but MiNI students solved the encryption problem and provided us with the required information. Used encryption:
	-BookingDatabase: FrameCodec (n = 2) -> ReverseCodec -> CezarCodec (n = -1) -> SwapCodec
	-ShutterStockDatabase: CezarCodec (n = 4) -> FrameCodec (n = 1) -> PushCodec (n = -3) -> ReverseCodec
	-TripAdvisorDatabase: PushCodec (n = 3) -> FrameCodec (n = 2) -> SwapCodec -> PushCodec (n = 3)
	-OysterDatabase: Nothing (no data to encode / decode)
, where the arrows indicate the direction of coding, i.e. to decode it is necessary to perform reverse operations from the end.

Each codec only works on numbers with non-negative integers (you must break the number down into digits before using). Descriptions of coding (encryption) of individual codecs:
	-FrameCodec - encapsulates a sequence of digits with a frame of consecutive numbers of length n, where 0 <= n <= 9. For n = 3 777777-> 123777777321
	-ReverseCodec - reverse the sequence of the digits. 123456-> 654321
	-PushCodec - moves digits to the right by n wrapping a sequence of digits, where n is arbitrary. For n = 3 112233445566-> 566112233445
	-CezarCodec - increase digits by n mod10 (the digit must remain a digit), where n is arbitrary. For n = 3 56789-> 89012
	-SwapCodec - each subsequent pair of digits is exchanged. If there is an odd number of digits, it remains unchanged lately. 1234567-> 2143657

Examples of encryption:
	-BookingDatabase: 5 -> 12521 -> 12521 -> 01410 -> 10140, 199 -> 1219921 -> 1299121 -> 0188010 -> 1088100
	-ShutterStockDatabase: 1080 -> 5424 -> 154241 -> 241154 -> 451142, 1920 -> 5364 -> 153641 -> 641153 -> 351146
	-TripAdvisorDatabase: 75 -> 57 -> 125721 -> 217512 -> 512217, 255 -> 255 -> 1225521 -> 2152251 -> 2512152

Requirements:
	-A single codec does not know if it encrypts real data or encrypted (does not know if it is another element of the encryption chain).
	-In order to check the correctness of the decryption, the decrypted number should be encrypted and check that the result is identical to the input number. If there are differences, skip the incorrect data. This is not a method that gives a lot of information, but it has a chance to find an error in the code.
	-You cannot use loops to create the encoding / decoding chain.


---TRAVEL AGENCIES---

Each of the three travel agencies specializes in a different country (Poland, France, Italy), each of which partially filters the data. Travel agencies create instances of trips, photos and reviews (which are not related in any way). The trip consists of a random number of days (1-4), and each day with 3 attractions and accommodation. The prepared data will have to be presented in a special way.
	-PolandTravel:
		1) Photos are omitted if the longitude (Longitude) is not in the range [14.4,23.5] and the latitude (Latitude) in [49.8,54.2].
		2) Attractions are omitted if they are not from Poland ("Poland").
		4) For credibility when displaying review content and user names, all instances of the letters 'e', ​​'a' are replaced with 'ę', 'ą'.
		3) For credibility when displaying photo names, all instances of the letters 's', 'c' are replaced with 'ś', 'ć'.
	-ItalyTravel:
		1) Photos are omitted if the longitude is not in the range [8.8,15.2] and the latitude is in [37.7,44.0].
		2) Attractions are omitted if they are not from Italy ("Italy").
		3) For credibility when displaying, the word "Dello_" is added to the name of the photos.
		4) For credibility when displaying, the word "Della_" is added to the username first.
	-FranceTravel:
		1) Photos are omitted if the longitude is not in the range [0.5.4] and the latitude is in [43.6,50.0].
		2) Attractions are omitted if they are not from France ("France").
		3) For credibility when displaying the review content, words shorter than 4 characters are converted into the word "la".

Requirements:
	-Rules 3) 4) cannot be applied to data (changed data cannot be stored). Each time they are applied when displayed.
	-Trips, photos and reviews must be separate classes for each travel agency.
	-It should be easy to add new travel agencies.
	-There already is ITravelAgency, which has to be implemented by travel agencies. It cannot be modified.

---ADVERTISING AGENCIES---

Trips created by travel agencies are placed on the Website (Offer Website) as offers created by Advertising Agencies. The offers are divided into graphic and text:
	-Graphic offer consists of a trip and photo collection given by the travel agency.
	-Text offer consists of a trip and collection of reviews given by the travel agency.
Each of the two types of offers can be permanent or temporary, i.e. it can be displayed only a predetermined number of times, later the offer expires (the message "This offer is expired" is printed).

Offers from various advertising agencies may appear on the website. Advertising agencies are not connected with any particular travel agency, but specialize in specific types of offers. There are two types of advertising agencies:
	-One specialize in text offers.
	-Second create graphic offers.

Requirements:
	-Each advertising agency creates offers of only one type (graphic or text).
	-Each advertising agency can place both temporary and permanent offers. Each advertising agency creates offers with a certain number of photos / reviews and with a certain limit on the display of temporary offers.
	-Advertising agency uses data provided by the travel agency for each offer. A different travel agency can be used for each offer, but data from one travel agency is used as part of one offer.
	-Offers can be created independently of the website.
	-It should be easy to add new advertising agencies.
	-Each type of offer should be represented by a separate class.


---PROGRAM---

The program works by repeatedly displaying (writing on the console) subsequent pages with offers. In each loop (while loop in the Run method) create a page with offers of different types (permanent and temporary) from various advertising agencies and travel agencies, and then display all offers 3 times to show that some temporary offers have expired. Pressing [q] or [Esc] closes the program, and every other key goes to the next page. Pages are not saved anywhere, i.e. you cannot return to those previously displayed.

"Displaying" a photo is writing its name and size. The display should look like the example, but it does not have to be reflected as to the spaces, because this is not the subject of the task. The price of the trip is the sum of the prices of attractions and accommodation. The trip rating is an average of the attractions and accommodation ratings.

Requirements:
	-You cannot modify anything in the Init folder.
	-Main function cannot be modified.
	-In the Program class, the Run function can only be modified in the designated place.