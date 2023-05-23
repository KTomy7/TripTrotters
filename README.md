# Industrial Informatics Project - TripTrotters 
### Team Members:

Kiss Tamás-Norbert ~ Team Leader 

Ielciu Cristian-Alexandru ~ Database Designer

Incer Maia ~ Software developer

Laza Oana-Stefania ~ Software developer

Ciubuc Rareș-Ovidiu ~ Software tester

 
## Chapter 1: Introduction

### 1.1 Description

Welcome to TripTrotters, your ultimate destination for all your travel needs! Whether you're a seasoned globetrotter or a first-time adventurer, Triptrotters is here to provide you with exceptional travel experiences tailored to your preferences.
	
At TripTrotters, we understand that travel is more than just visiting new places—it's about immersing yourself in unique cultures, discovering hidden gems, and creating lifelong memories. With that being said, one of the most important things for having a great experience while travelling is finding a comfortable accommodation. With our knowledge in the travel industry, we strive to curate extraordinary journeys that capture the essence of each destination.

Our user-friendly website offers a wide range of services to enhance your travel planning process. Begin your journey by browsing through our meticulously curated collection of destinations from around the world. Whether you're dreaming of exploring exotic landscapes, indulging in culinary delights, or embarking on thrilling adventures, Triptrotters has the perfect destination for you.

### 1.2	Goals

The main goal of the TripTrotters application is to provide users with a platform to find the best offers, accommodations, and destinations for their holidays. The application serves both individuals looking for accommodations and travel agents who create and sell offers to customers. Additionally, the application aims to expand its services to include hotels and inns in the future.

### 1.2.1 Accommodation Rental

One of the core features of the TripTrotters application is the ability for owners to rent their apartments. This feature allows property owners to list their apartments on the platform, providing detailed information about the accommodations, such as location, amenities, and pricing. Users looking for accommodations can browse through the listings, view photos, and make bookings based on their preferences.

By facilitating apartment rentals, TripTrotters offers individuals a convenient way to monetize their properties and generate income by renting them out to travelers and tourists.

### 1.2.2 Offers Creation and Sales

In addition to apartment rentals, TripTrotters provides travel agents with the ability to create and sell offers to customers. Travel agents can use the application to design attractive holiday packages, including accommodations. These offers can be tailored to specific destinations or themes, such as beach vacations, city tours, or adventure trips.

By offering a platform for travel agents to promote their offers, TripTrotters connects them with potential customers who are seeking pre-packaged holiday experiences. This benefits both agents and customers, as agents can reach a wider audience, and customers can easily compare and choose from various offers based on their preferences and budget.

### 1.2.3 Expansion to Hotels and Inns

While currently focused on apartment rentals and travel agency offers, TripTrotters has plans to expand its services to include hotels and inns. This expansion will provide users with a wider range of accommodation options, catering to different preferences, budgets, and travel styles.

By incorporating hotels and inns into the platform, TripTrotters aims to become a comprehensive marketplace for holiday accommodations. This expansion will further enhance the user experience by offering more choices and ensuring that users can find the perfect accommodation that suits their needs and preferences.

## Chapter 2: Application Design

### 2.1	Functionalities and features


TripTrotters offers a range of functionality designed to cater to the needs of owners, agents, and travelers. Users can create accounts based on their role, log in securely, and access the features specific to their account type. Travelers can create posts, leave reviews, and comment on posts to share their experiences and engage with others. Agents can create and sell holiday offers, while owners can list and rent their apartments. The TripTrotters platform promotes interaction, engagement, and collaboration among users, facilitating the search for accommodations and holiday packages.

### 2.1.1 Create Account

TripTrotters offers three different types of user accounts:

1. Owner: Property owners can create an account to list and rent their apartments through the platform.
2. Agent: Travel agents can create an account to create and sell holiday offers to customers.
3. Traveler: Individuals looking for accommodations and holiday packages can create a traveler account.

Each account type has different privileges and access to specific features based on their role within the application.

### 2.1.2 Login

Users can securely log in to their TripTrotters accounts using their registered credentials, such as email address and password. The login functionality ensures that users can access their personalized profiles and take advantage of the available features.

### 2.1.3 Create Posts (Traveler)

Travelers can create posts to share their holiday experiences, recommendations, or seek advice from the TripTrotters community. The post creation feature allows users to write a title, description, and optionally attach images to enhance their posts. Once created, these posts are visible to other users who can like, comment, and provide reviews on them.

### 2.1.4 Create Offers (Agent)

Agents have the ability to create and sell holiday offers through the TripTrotters application. They can create attractive packages that include accommodations, transportation, activities, and other services. The offer creation feature allows agents to define the details of the offer, set pricing, specify availability, and provide a description with images. These offers are visible to travelers who can browse and book them based on their preferences.

### 2.1.5 Create Apartments (Owner)

Owners can create listings for their apartments, allowing them to rent out their properties through TripTrotters. The apartment creation feature enables owners to provide detailed information about their accommodations, such as location, amenities, pricing, availability, and upload photos. Travelers can view these listings, compare options, and make bookings directly through the platform.

### 2.1.6 Like Posts

Users, including travelers, agents, and owners, can like posts created by travelers to show appreciation or agreement. The like feature allows users to indicate their interest in a particular post and helps to identify popular or trending content within the community.

### 2.1.7 Leave Reviews

Travelers can leave reviews for accommodations they have stayed in, sharing their experiences, ratings, and feedback. Reviews provide valuable insights for other travelers when considering booking accommodations and help maintain transparency and trust within the TripTrotters community.

### 2.1.8 Comment on Posts

Users can engage in discussions and conversations by commenting on posts created by travelers. This feature enables users to ask questions, share additional information, or provide feedback related to the content of the post. Commenting encourages interaction and fosters a sense of community among TripTrotters users.

### 2.1.9 Edit Comments
Users have the ability to edit their own comments on posts. This feature allows users to make changes or add additional information to their comments after they have been submitted. By providing the option to edit comments, TripTrotters ensures that users can refine their contributions and maintain the accuracy and relevance of their comments over time.

### 2.1.10 Delete Comments

Users can delete their own comments on posts if they no longer wish to have their comments displayed or if they want to remove content they no longer stand by. The delete comment feature allows users to exercise control over their contributions and maintain the quality and appropriateness of the comment section.

## Chapter 3: Database Design

![image](https://github.com/KTomy7/TripTrotters/assets/101259596/435cd735-3a19-4da1-8924-75bad14b9a43)
<p align="center">Figure 1. Database Diagram</p>

The provided code represents a set of models/classes for the TripTrotters application. Let's explain the relationships between these models and their properties:

1. Address:
   - Represents an address entity with properties such as ‘Id’, ‘Country’, ‘City’, ‘Street’, ‘StreetNumber’.
   - It has a one-to-one relationship with the ‘Apartment’ model through the ‘Apartment’ property.

2. Apartment:
   - Represents an apartment entity with properties like ‘Id’, ‘Title’, ‘Description’, ‘Price’.
   - It has a foreign key relationship with the ‘Address’ model through the ‘AddressId’ property.
   - It has a foreign key relationship with the ‘User’ model (presumably representing the owner) through the ‘OwnerId’ property.
   - It has many-to-one relationships with the ‘Review’, ‘Post’, ‘Offer’, and ‘Image’ models through the corresponding navigation properties.
   - It also has one-to-many relationships with ‘Review’, ‘Post’, ‘Offer’, and ‘Image’ models through the corresponding navigation properties.

3. Comment:
   - Represents a comment entity with properties like ‘Id’, ‘Like’, ‘Date’, ‘Text’.
   - It has a foreign key relationship with the ‘User’ model through the ‘UserId’ property.
   - It has a foreign key relationship with the `Post` model through the ‘PostId’ property.
   - It has a many-to-many relationship with the ‘UserCommentLike’ model through the ‘UsersLikes’ navigation property.

4. Image:
   - Represents an image entity with properties like ‘Id’ and ‘ImageUrl’.
   - It has optional foreign key relationships with the `Apartment` and ‘Post’ models through the ‘ApartmentId’ and ‘PostId’ properties, respectively.
   - It has corresponding navigation properties to access the associated ‘Apartment’ and ‘Post’ entities.

5. Offer:
   - Represents an offer entity with properties like ‘Id’, ‘Description’, ‘Title’, ‘StartDate’, ‘EndDate’.
   - It has a foreign key relationship with the ‘Agent’ model (presumably representing the agent) through the ‘AgentId’ property.
   - It has a foreign key relationship with the ‘Apartment’ model through the ‘ApartmentId’ property.

6. Post:
   - Represents a post entity with properties like ‘Id’, ‘Title’, ‘Description’, ‘Budget’, ‘Likes’, ‘Date’.
   - It has a foreign key relationship with the ‘Apartment’ model through the ‘ApartmentId’ property.
   - It has a foreign key relationship with the `User` model through the ‘UserId’ property.
   - It has a one-to-many relationship with the ‘Comment’ and ‘Image’ models through the corresponding navigation properties.
   - It also has a many-to-many relationship with the ‘UserPostLike’ model through the ‘UsersLikes’ navigation property.

7. Review:
   - Represents a review entity with properties like ‘Id’, ‘Description’, ‘Rating’, ‘Date’.
   - It has a foreign key relationship with the `User` model through the ‘UserId’ property.
   - It has a foreign key relationship with the ‘Apartment’ model through the ‘ApartmentId’ property.

8. User:
   - Represents a user entity with properties inherited from ‘IdentityUser<int>’, including properties like ‘Id’, ‘UserName’, ‘Email’, etc.
   - It has properties like ‘ImageUrl’.
   - It has one-to-many relationships with the ‘Post’, ‘Offer’, ‘Review’, and ‘Comment’ models through the corresponding navigation properties.
   - It also has a one-to-many relationship


## Chapter 4: Graphic Interface design and functionality

The TripTrotters' Graphic Interface has been thoughtfully designed to cater to the needs of different user types, namely travelers, owners, and agents. While the home page offers a unified experience for all users, their actions and abilities differ based on their respective roles and the content they can create, such as posts, apartments, and offers. Users belonging to other user types can still enjoy the visual content and interact with it.
 
![image](https://github.com/KTomy7/TripTrotters/assets/101259596/1f4628ca-721e-493a-b3ee-a458d6250618)
![image](https://github.com/KTomy7/TripTrotters/assets/101259596/11abd0fb-41b7-4c66-a0ad-6c91be24d036)
<p align="center">Figure 2. Home Page</p>

To get started, users are directed to the register and login page, which serves as a common entry point for all user types. Here, users have the option to create an account by providing their email address, password, username, and profile picture, ensuring a personalized and engaging experience.

![image](https://github.com/KTomy7/TripTrotters/assets/101259596/c4dfaf5e-b587-40ce-9c15-5030d439ae75)
<p align="center">Figure 3. Register Page</p>
					
![image](https://github.com/KTomy7/TripTrotters/assets/101259596/a1d3c0e1-4f6e-4787-82d7-6065fb4f2053)
<p align="center">Figure 4. Login Page</p>			

At the top of the TripTrotters' page, you will find a fixed navigation bar that remains visible across all user pages. The navigation bar provides convenient access to various sections, including posts, offers, and apartments. Additionally, users can access their profile and perform a logout action.

By navigating to the profile page through the navbar, users can view and manage their personal information. They can also browse through their favorite posts, explore offers, and access their customized apartment recommendations.

![image](https://github.com/KTomy7/TripTrotters/assets/101259596/ec4762dc-b4c4-4b94-8afb-d80a4dc389c8)
![image](https://github.com/KTomy7/TripTrotters/assets/101259596/1fbf03aa-56dc-4390-84bb-6b33ed779f37)
 <p align="center">Figure 5. Posts Page</p>

The page has a "feed" section that contains multiple posts. Each post is displayed in a container with a post body. 
The post detail section includes a carousel that allows users to view multiple images related to the post. There are carousel indicators at the bottom of the carousel to navigate through the images. 

Under the post detail section, there is a likes and comment preview section. The likes preview shows the number of likes the post has received.The comment preview displays the number of comments the post has received and includes a button to view the comments. 
Overall, this page provides a platform for users to view and interact with posts, including liking, commenting, and editing comments.

 
![image](https://github.com/KTomy7/TripTrotters/assets/101259596/faecc138-15d7-4fa9-8ce4-87e86911bc63)
<p align="center">Figure 6. Post Creation</p>
	
This web page allows travelers to create posts about specific existing apartments.

On the Offers page, exclusive access is granted to agents who specialize in offering various travel services and accommodations. Agents can create enticing offers tailored to the needs of travelers. These offers may include package deals, special promotions, or unique experiences that cater to different travel preferences.
On the Apartments page, the focus is on individual property owners who want to showcase their available apartments for travelers. Owners have the opportunity to provide detailed descriptions of their apartments, highlighting the amenities, features, and distinctive qualities of each property.
The creation process for offers and apartments involves filling out comprehensive forms with relevant information. Agents and owners are prompted to provide essential details such as location, pricing, availability, and any additional services or benefits (showed in the description) that come with the offer or apartment.
 
![image](https://github.com/KTomy7/TripTrotters/assets/101259596/b62e5978-eda9-471c-901b-9e806b04cd5f)
<p align="center">Figure 7. Apartment Creation</p>

![image](https://github.com/KTomy7/TripTrotters/assets/101259596/bfb14e37-15e8-4486-99e7-fa2b737c0d87)
 <p align="center">Figure 8.Offer Creation</p>

## Chapter 5: Application Implementation

### 5.1 Work Procedure

To begin the development of the application, we initiated the process by creating a new project in Visual Studio 2022. Our choice of programming language was C#, and we opted for the ASP .NET MVC Web Application framework. Since our approach involved implementing a code-first methodology, our initial focus was on defining the necessary classes that formed the foundation of the application's model.

Once the classes were established, we proceeded to generate the MSSQL database using Entity Framework, leveraging the defined model. This automated process facilitated the creation of the database structure and ensured its synchronization with the model classes.

Simultaneously, we started working on the implementation of controllers and views. Initially, our primary focus was on the basic CRUD (Create, Read, Update, Delete) operations, which allowed for the management of the application's data. As the development progressed, we gradually introduced more sophisticated functionalities to enhance the overall user experience.

Throughout this iterative process, we paid close attention to maintaining parallel development of both controllers and views, ensuring their seamless integration. This approach allowed us to incrementally build and refine the application's features, taking into account the evolving requirements and user feedback.

By starting with the fundamental CRUD operations, we established a solid foundation for data management within the application. As we advanced, we incorporated more complex functionalities, such as advanced search capabilities, data validation, and user authentication.

In the following section, you can see the implementation of each model class in detail:

### Adding Images: Using Cloudinary API

Cloudinary is a cloud-based media management platform that offers a comprehensive set of APIs for image and video manipulation, optimization, and storage.
Configuration:
In the project, we located the configuration file where we stored API credentials(appsettings.json). Then added the Cloudinary API credentials obtained from my Cloudinary account (cloud name, API key, and API secret) to this configuration file.
 
Uploading an Image:
To upload an image using the Cloudinary API:
1.	Add an image upload form or user interface element in the MVC project's view.
 

2.	In the corresponding controller action or method, receive the uploaded image data: 
 
3.Pass the image data to the Cloudinary API along with any desired transformation options.
4.Retrieve the response from the Cloudinary API, which typically includes the uploaded image's public URL.

Employing a code-first implementation approach offered significant advantages, particularly in terms of managing changes to the model and their seamless integration with the database through migrations.

By adopting the code-first methodology, we could focus on defining and refining the model classes based on the evolving requirements of the application. As we made changes to the model, such as adding new properties, modifying relationships, or introducing additional entities, these adjustments could be effortlessly reflected in the database structure.

By utilizing the TripTrottersDbContext class and its configurations, the application can interact with the database seamlessly. It provides a bridge between the application's model classes and the underlying database tables, allowing data to be persisted, retrieved, and manipulated using Entity Framework Core's capabilities.

The connection between the application and the database was established through the ‘TripTrottersDbContext’ class. 

1. First, the necessary dependencies were imported using the ‘using’ statements. These include ‘Microsoft.AspNetCore.Identity’ for user authentication and authorization, ‘Microsoft.AspNetCore.Identity.EntityFrameworkCore’ for working with Identity-related entities, and ‘Microsoft.EntityFrameworkCore’ for Entity Framework Core functionality.

2. The ‘TripTrottersDbContext’ class was created as a subclass of ‘IdentityDbContext<User, IdentityRole<int>, int>’. This class extends the ‘IdentityDbContext’ provided by ASP.NET Core Identity and is responsible for managing the application's data and relationships.

3. In the constructor of ‘TripTrottersDbContext’, an instance of ‘DbContextOptions<TripTrottersDbContext>’ is passed as a parameter. This options object is used to configure the database connection and other options for the context.

4. The ‘OnConfiguring’ method is overridden to configure the database provider and connection string. Here, the code specifies the SQL Server provider (‘UseSqlServer’) and provides the connection string with the necessary details such as the server, database name, credentials, etc.

5. The ‘OnModelCreating’ method is overridden to define the relationships and constraints between the entities in the model. This method is responsible for configuring the database schema based on the entity classes and their relationships. In this code, various configurations are specified using the ‘modelBuilder.Entity<T>()’ calls, which define relationships between different entities using methods like ‘HasOne’, ‘WithMany’, ‘HasForeignKey’, etc.

6. Finally, the ‘DbSet<T>’ properties in the ‘TripTrottersDbContext’ class represent the entities that will be mapped to database tables. Each ‘DbSet<T>’ corresponds to a table in the database, and the entity type ‘T’ represents the structure and properties of the table.

With this setup, the ‘TripTrottersDbContext’ class acts as a bridge between the application and the database. It allows the application to interact with the database by providing CRUD (Create, Read, Update, Delete) operations and handling the mapping of entities to the corresponding database tables.

### 5.2 Application Structure

The file structure of our application follows the Model-View-Controller (MVC) architectural pattern, which promotes a clear separation of concerns and modular organization of code. This structure helps in organizing and managing the different components of the application effectively.

The MVC pattern consists of three main components:

1. Models: The models represent the data and the business logic of the application. They encapsulate the application's data structures and define the rules and operations associated with that data. In our application, the model classes are located in the "Models" directory. These classes define the entities such as users, posts, comments, apartments, offers, etc., and their relationships.

2. Views: The views are responsible for presenting the data to the users and handling the user interface. In our application, the view files are typically located in a "Views" directory. These files contain HTML templates (using bootstrap components). Views are used to render the user interfaces for different pages and display the data retrieved from the controllers.

One suggestive page contained in the views section is the home page which has the following components:
Background Image: The home page features an attractive background image that sets the tone for a travel-oriented experience. It creates a visually appealing atmosphere and captures the user's attention.

Masthead Section: The masthead section is the main focal point of the page. It contains a bold and prominent title "TripTrotters," which showcases the brand identity. Below the title, there is a descriptive paragraph welcoming users to TripTrotters and emphasizing its role as the ultimate destination for all travel needs. The text highlights the personalized travel experiences offered by the platform.

"Get to know us" Button: A call-to-action button labeled "Get to know us" is provided, encouraging users to learn more about the platform. When clicked, it smoothly scrolls down to the "About" section on the same page, where users can explore details about the team.

Social Icons: The home page includes a set of social icons in a separate section. These icons represent key features of the application, such as Apartments, Posts, and Offers. Users can click on these icons to access the corresponding sections of the application and explore the related content.

About Section: The "About" section provides information about the TripTrotters team. It displays the team members' profile pictures, names, roles within the team, and a brief description of their interests and responsibilities. Each team member's details are accompanied by a button that allows users to view more information about them, typically linking to their respective GitHub profiles.
 
3. Controllers: The controllers handle the requests from the users, interact with the models to retrieve or manipulate data, and determine the appropriate view to render. In our application, the controller classes are typically located in a "Controllers" directory. These classes contain methods (actions) that are responsible for processing incoming requests, performing necessary operations, and returning the appropriate response. Controllers are responsible for coordinating the flow of data between the models and views.

The Post controller stands out as a notable controller within our application due to its complexity and extensive functionality. It encompasses a wide range of operations, making it the most intricate controller among all.
The PostController is a controller class that handles HTTP requests and manages the actions related to posts in the TripTrotters application. It is built using the Microsoft ASP.NET Core MVC framework.
 
 
IPostService: An interface representing a service for managing posts.
IApartmentService: An interface representing a service for managing apartments.
ICommentService: An interface representing a service for managing comments.
IHttpContextAccessor: An interface providing access to the current HttpContext.
ICloudinaryImageService: An interface representing a service for interacting with Cloudinary for image management.
IImageService: An interface representing a service for managing images.
IUserPostLikeService: An interface representing a service for managing user likes on posts.
These dependencies are injected into the controller through constructor injection.

Index
URL: /Post/Index
HTTP Method: GET
This action retrieves all the posts from the IPostService and assigns the corresponding comments to each post using the ICommentService. It then returns a view with the list of posts.

Detail
URL: /Post/Detail/{id}
HTTP Method: GET
This action retrieves the post with the specified id from the IPostService and returns a view to display the details of the post.

Create
URL: /Post/Create
HTTP Method: GET
This action is responsible for rendering the view to create a new post. It retrieves the current user's ID from the IHttpContextAccessor and initializes a CreatePostViewModel with the user ID. The view is then returned.

URL: /Post/Create
HTTP Method: POST
This action is responsible for handling the form submission when creating a new post. It takes a CreatePostViewModel object as a parameter and, if the model state is valid, it creates a new Post object using the provided data and adds it to the IPostService. It also uploads any images associated with the post using the ICloudinaryImageService and adds corresponding Image objects to the IImageService. Finally, it redirects to the Index action.

Edit
URL: /Post/Edit/{id}
HTTP Method: GET
This action is responsible for rendering the view to edit a post with the specified id. It retrieves the post from the IPostService and initializes an EditPostViewModel with the post data. The view is then returned.

URL: /Post/Edit/{id}
HTTP Method: POST
This action is responsible for handling the form submission when editing a post. It takes the id of the post to be edited and an EditPostViewModel object as parameters. If the model state is valid, it retrieves the post from the IPostService, updates its data with the provided values, and calls the Update method of the IPostService to save the changes. Finally, it redirects to the Index action.

UpdateLike
URL: /Post/UpdateLike/{id}
HTTP Method: POST
This action is responsible for updating the number of likes on a post. It takes the id of the post and an EditPostViewModel object as parameters. It retrieves the post from the IPostService and checks if the current user has already liked the post using the IUserPostLikeService. If the user has liked the post, the like is removed by decrementing the like count and deleting the corresponding UserPostLike object. If the user has not liked the post, a new like is added by incrementing the like count and creating a new UserPostLike object. Finally, the post is updated using the IPostService and the action redirects to the Index action.

Delete
URL: /Post/Delete/{id}
HTTP Method: GET
This action is responsible for rendering the view to confirm the deletion of a post with the specified id. It retrieves the post from the IPostService and returns the view.

URL: /Post/Delete/{id}
HTTP Method: POST
This action is responsible for handling the deletion of a post. It takes the id of the post to be deleted as a parameter. It retrieves the post from the IPostService and deletes it using the Delete method of the IPostService. Finally, it redirects to the Index action.

By organizing the application files based on the MVC framework, we achieve a modular structure that promotes code reusability, maintainability, and separation of concerns. This structure allows for easier collaboration among team members and enhances the scalability and extensibility of the application.

## Chapter 6: Application Testing

During our discussion on the "Testing diagram" approach, we made the decision to commence work on it prior to the complete finalization of the application. This proactive step allowed us to identify potential issues pertaining to certain features of our application, which we promptly resolved and implemented from the beginning.
	
![image](https://github.com/KTomy7/TripTrotters/assets/101259596/b0e41235-af1e-4349-b602-fe7f657246a6)

Regarding the actual testing of the application, we initially focused on the initial features that were already functional, including the user registration and login experience. From there, we systematically progressed through testing all the features encompassed within our application.
   
## Chapter 7: Conclusions

Taking into account the comprehensive range of functionalities presented, their intricate design, and the substantial amount of data handled, TripTrotters has undeniably achieved its objective. 
Furthermore, this project paves the way for future expansions in the realm of travel and accommodation. As a versatile traveling platform, TripTrotters allows users to explore and discover apartments, create engaging posts based on their experiences, leave valuable reviews, and even generate attractive offers. With its broad applicability in various social settings, TripTrotters holds tremendous potential to make the desired impact our team envisioned. It opens up possibilities for personalized, professional, and even medical travel needs, thereby enriching the overall user experience and further establishing TripTrotters as a go-to platform for travel enthusiasts.
