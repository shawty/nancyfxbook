# nancyfxbook
Sample application to accompany my NancyFX succinctly eBook published by Syncfusion

You can download the book from the Syncfusion eBook portal at

http://www.syncfusion.com/resources/techportal/details/ebooks/nancyfx

Instructions for the MP3Player sample
=====================================
The MP3 Player needs a database of tracks, mp3's, artists etc.

The 'DemoData' project is used to access this database using plain old ADO.NET.

In the 'Sql' folder in the project, you'll find the scripts I used to create mine in SQL Server 2008, and as with any data driven project you will have to also enter a valid connection string in the main web.config file too.

Once you do that, you'll then need to add some data to the new database listing the files available to play.

If you do not do this step the MP3 player will not work.
