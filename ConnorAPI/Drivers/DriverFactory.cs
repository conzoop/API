
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConnorAPI.Drivers
{
    public class TrelloApiDriver
    {

        string apiKey = "d67a55072cf473900fd3c72dddee7164";
        string apiToken = "ATTAd64501a55fecf791648769af2e4fe470942412a4af7193e54b73012d330082929097BAB1";
        string boardId = "6550e6aedd319dae007b75a7";
        string todoListId = "YOUR_LIST_ID";
        string doneListId = "YOUR_LIST_ID_TWO";
        string cardId = "YOUR_CARD_ID";


        public HttpStatusCode HttpStatusCode { get; set; }

        RestClientOptions BaseUrl = new RestClientOptions("https://api.trello.com/1/");
        private readonly string ApiKey;
        private readonly string ApiToken;


        [Test, Order(1)]

        public void CreateNewBoard()
        {
            RestClient restClient = new RestClient(BaseUrl);
            RestRequest restRequest = new RestRequest("boards/");
            restRequest.AddQueryParameter("key", apiKey);
            restRequest.AddQueryParameter("token", apiToken);
            restRequest.AddQueryParameter("name", "ConnorsBoard");
            RestResponse restResponse = restClient.Post(restRequest);
            string BoardName = restResponse.Content.ToString();
            var boardInfo = JsonConvert.DeserializeObject<Objects>(BoardName);
            var board = boardInfo.Id;

            RestClient restClient2 = new RestClient(BaseUrl);
            RestRequest restRequest2 = new RestRequest("boards/");
            restRequest2.AddQueryParameter("key", apiKey);
            restRequest2.AddQueryParameter("token", apiToken);
            restRequest2.AddUrlSegment("id", board);
            RestResponse restResponse2 = restClient.Get(restRequest);
            HttpStatusCode = restResponse.StatusCode;
            int statCode = (int)HttpStatusCode;
            Assert.AreEqual(200, statCode);
        }


        [Test, Order(2)]

        public void CreateNewTODOList()
        {
            RestClient restClient = new RestClient(BaseUrl);
            RestRequest restRequest = new RestRequest("lists/");
            restRequest.AddQueryParameter("key", apiKey);
            restRequest.AddQueryParameter("token", apiToken);
            restRequest.AddQueryParameter("name", "TODO");
            restRequest.AddQueryParameter("idBoard", boardId);

            RestResponse restResponse = restClient.Post(restRequest);
            string listName = restResponse.Content.ToString();
            var listInfo = JsonConvert.DeserializeObject<Objects>(listName);
            todoListId = listInfo.Id;
            HttpStatusCode = restResponse.StatusCode;
            int statCode = (int)HttpStatusCode;
            Assert.AreEqual(200, statCode);
        }

        [Test, Order(3)]
        public void CreateNewDONEList()
        {
            RestClient restClient = new RestClient(BaseUrl);
            RestRequest restRequest = new RestRequest("lists/");
            restRequest.AddQueryParameter("key", apiKey);
            restRequest.AddQueryParameter("token", apiToken);
            restRequest.AddQueryParameter("name", "DONE");
            restRequest.AddQueryParameter("idBoard", boardId);

            RestResponse restResponse = restClient.Post(restRequest);
            string listName = restResponse.Content.ToString();
            var listInfo = JsonConvert.DeserializeObject<Objects>(restResponse.Content);
            doneListId = listInfo.Id;
            HttpStatusCode = restResponse.StatusCode;
            int statCode = (int)HttpStatusCode;
            Assert.AreEqual(200, statCode);
        }

        [Test, Order(4)]
        public void CreateNewCard()
        {
            RestClient restClient = new RestClient(BaseUrl);
            RestRequest restRequest = new RestRequest("cards/");
            restRequest.AddQueryParameter("key", apiKey);
            restRequest.AddQueryParameter("token", apiToken);
            restRequest.AddQueryParameter("name", "Connor Card One");
            restRequest.AddQueryParameter("idList", todoListId);

            RestResponse restResponse = restClient.Post(restRequest);

            HttpStatusCode = restResponse.StatusCode;
            int statCode = (int)HttpStatusCode;
            Assert.AreEqual(200, statCode);

            var cardInfo = JsonConvert.DeserializeObject<Objects>(restResponse.Content);
            cardId = cardInfo?.Id;
        }

        [Test, Order(5)]
        public void MoveCard()
        {
            {
                RestClient restClient = new RestClient(BaseUrl);
                RestRequest restRequest = new RestRequest($"cards/{cardId}/idList");
                restRequest.AddQueryParameter("key", apiKey);
                restRequest.AddQueryParameter("token", apiToken);
                restRequest.AddQueryParameter("value", doneListId); 

                RestResponse restResponse = restClient.Put(restRequest); 

                HttpStatusCode = restResponse.StatusCode;
                int statCode = (int)HttpStatusCode;
                Assert.AreEqual(200, statCode);
            }

        }
    }
}
