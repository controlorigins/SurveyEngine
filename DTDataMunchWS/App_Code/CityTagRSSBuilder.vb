Imports Microsoft.VisualBasic
Imports LINQHelper.System.Linq.Dynamic


Public Class CityTagRSSBuilder
    Public Shared Function GetXML(ByVal Tags As String) As String

        Dim mydb As New CityTagDataContext

        Dim mycoll As New List(Of LinkedContent)
        Dim SearchString As New StringBuilder
        Dim x As New ContentTag
        ' x.Tag.Name
        Dim mySearchTags As String() = {""}

        Try
            mySearchTags = Tags.Split(",")
        Catch ex As Exception
            Tags = "Austin,Texas,Restaurants"
            mySearchTags = Tags.Split(",")
        End Try

        Dim a As Integer = 1
        For Each i In mySearchTags

            If a < mySearchTags.Length Then
                SearchString.AppendFormat("Tag.Name=""{0}"" OR ", i)
            Else
                SearchString.AppendFormat("Tag.Name=""{0}"" ", i)
            End If
            a += 1
        Next

        Dim mycolltemp = mydb.ContentTags.Where(SearchString.ToString).ToList

        For Each item In mycolltemp
            If CountOccurenceOfValue(mycolltemp, item.LinkedContentID) = mySearchTags.Length Then
                Dim tempitem = (From i In mydb.LinkedContents Where i.Id = item.LinkedContentID).Single
                If String.IsNullOrEmpty(tempitem.IconURL) Then tempitem.IconURL = String.Format("http://ws.cityinformationcenter.com/images/{0}", "default.png")
                mycoll.Add(tempitem)
            End If
        Next
        mycoll = (From i In mycoll.Distinct Distinct).ToList
        mycoll = (From i In mycoll Where i.Active = True).ToList



        Dim outputxml = <?xml version="1.0" encoding="UTF-8"?>
                        <rss version="2.0">
                            <channel>
                                <title>City Highlights</title>
                                <description>Favorites</description>
                                <link></link>
                                <rawurl><%= HttpContext.Current.Request.RawUrl %></rawurl>
                                <tags><%= Tags %></tags>
                                <lastBuildDate><%= Now.ToLongDateString %></lastBuildDate>
                                <pubDate><%= Now.ToLongDateString %></pubDate>
                                <ttl>1800</ttl>
                                <%= From i In mycoll Select <item id=<%= i.Id %>>
                                                                <title><%= i.Title %></title>
                                                                <description><%= i.Description %></description>
                                                                <link><%= i.LinkURL %></link>
                                                                <image><%= i.IconURL %></image>
                                                                <keywords><%= From t In i.ContentTags Select <keyword id=<%= t.Tag.Id %>>
                                                                                                                         <%= t.Tag.Name %>
                                                                                                                     </keyword> %>
                                                                </keywords>
                                                                <author></author>
                                                                <guid isPermaLink="false"><%= Guid.NewGuid.ToString %></guid>
                                                                <pubDate><%= Now.ToLongDateString %></pubDate>
                                                            </item> %>
                            </channel>
                        </rss>




        Return outputxml.ToString()
    End Function
        
    Private Shared Function CountOccurenceOfValue(list As List(Of ContentTag), valueToFind As Integer) As Integer
        Return ((From temp In list Where temp.LinkedContentID = valueToFind).Count())
    End Function

End Class
