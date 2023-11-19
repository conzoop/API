
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConnorAPI.Drivers
{
    public class TrelloApiDriver
    {

        string apiKey = "d67a55072cf473900fd3c72dddee7164";
        string apiToken = "ATTAd64501a55fecf791648769af2e4fe470942412a4af7193e54b73012d330082929097BAB1";
        string boardId = "65579645bcbede8bb5899262";
        string todoListId = "655a38632d7578f0a4bcde59";
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
            RestRequest createBoardRequest = new RestRequest("boards/");
            createBoardRequest.AddQueryParameter("key", apiKey);
            createBoardRequest.AddQueryParameter("token", apiToken);
            createBoardRequest.AddQueryParameter("name", "ConnorsBoard");
            RestResponse createBoardResponse = restClient.Post(createBoardRequest);
            var boardInfo = JsonConvert.DeserializeObject<Objects>(createBoardResponse.Content);
            var boardId = boardInfo?.Id;

            RestRequest getBoardRequest = new RestRequest($"boards/{boardId}");
            getBoardRequest.AddQueryParameter("key", apiKey);
            getBoardRequest.AddQueryParameter("token", apiToken);

            RestResponse getBoardResponse = restClient.Get(getBoardRequest);
            string retrievedBoardDetails = getBoardResponse.Content.ToString();
            var retrievedBoard = JsonConvert.DeserializeObject<BoardDetails>(retrievedBoardDetails);
            string retrievedBoardId = retrievedBoard?.Id;
            string retrievedBoardName = retrievedBoard?.Name;

            Console.WriteLine($"Board ID: {retrievedBoardId}");
            Console.WriteLine($"Board Name: {retrievedBoardName}");
        }

        [Test, Order(2)]
        public void CreateNewTODOList()
        {
            RestClient restClient = new RestClient(BaseUrl);
            RestRequest createListRequest = new RestRequest("lists/");
            createListRequest.AddQueryParameter("key", "d67a55072cf473900fd3c72dddee7169");
            createListRequest.AddQueryParameter("token", apiToken);
            createListRequest.AddQueryParameter("name", "TODO");
            createListRequest.AddQueryParameter("idBoard", boardId);

            RestResponse createListResponse = restClient.Post(createListRequest);
            var listInfo = JsonConvert.DeserializeObject<Objects>(createListResponse.Content);
            var todoListId = listInfo?.Id;


            RestRequest getListRequest = new RestRequest($"lists/{todoListId}");
            getListRequest.AddQueryParameter("key", "d67a55072cf473900fd3c72dddee7169");
            getListRequest.AddQueryParameter("token", apiToken);
            RestResponse getListResponse = restClient.Get(getListRequest);
            string retrievedListDetails = getListResponse.Content.ToString();
            var retrievedList = JsonConvert.DeserializeObject<ListDetails>(retrievedListDetails);
            string retrievedListId = retrievedList?.Id;
            string retrievedListName = retrievedList?.Name;

            Console.WriteLine($"List ID: {retrievedListId}");
            Console.WriteLine($"List Name: {retrievedListName}");
        }

        [Test, Order(3)]
        public void CreateNewDONEList()
        {
            RestClient restClient = new RestClient(BaseUrl);
            RestRequest createDoneListRequest = new RestRequest("lists/");
            createDoneListRequest.AddQueryParameter("key", apiKey);
            createDoneListRequest.AddQueryParameter("token", apiToken);
            createDoneListRequest.AddQueryParameter("name", "DONE");
            createDoneListRequest.AddQueryParameter("idBoard", boardId);

            RestResponse createDoneListResponse = restClient.Post(createDoneListRequest);
            var listInfo = JsonConvert.DeserializeObject<Objects>(createDoneListResponse.Content);
            var doneListId = listInfo?.Id;

            RestRequest getDoneListRequest = new RestRequest($"lists/{doneListId}");
            getDoneListRequest.AddQueryParameter("key", apiKey);
            getDoneListRequest.AddQueryParameter("token", apiToken);
            RestResponse getDoneListResponse = restClient.Get(getDoneListRequest);
            string retrievedDoneListDetails = getDoneListResponse.Content.ToString();
            var retrievedDoneList = JsonConvert.DeserializeObject<ListDetails>(retrievedDoneListDetails);

            string retrievedDoneListId = retrievedDoneList?.Id;
            string retrievedDoneListName = retrievedDoneList?.Name;
            Console.WriteLine($"DONE List ID: {retrievedDoneListId}");
            Console.WriteLine($"DONE List Name: {retrievedDoneListName}");
        }

        [Test, Order(4)]
        public void CreateNewCardToDo()
        {
            RestClient restClient = new RestClient(BaseUrl);
            RestRequest createCardRequest = new RestRequest("cards/");
            createCardRequest.AddQueryParameter("key", apiKey);
            createCardRequest.AddQueryParameter("token", apiToken);
            createCardRequest.AddQueryParameter("name", "Connor Card One");
            createCardRequest.AddQueryParameter("idList", todoListId);

            RestResponse createCardResponse = restClient.Post(createCardRequest);
            var cardInfo = JsonConvert.DeserializeObject<Objects>(createCardResponse.Content);
            var cardId = cardInfo?.Id;

            RestRequest getCardRequest = new RestRequest($"cards/{cardId}");
            getCardRequest.AddQueryParameter("key", apiKey);
            getCardRequest.AddQueryParameter("token", apiToken);
            RestResponse getCardResponse = restClient.Get(getCardRequest);
            string retrievedCardDetails = getCardResponse.Content.ToString();
            var retrievedCard = JsonConvert.DeserializeObject<CardDetails>(retrievedCardDetails);
            string retrievedCardId = retrievedCard?.Id;
            string retrievedCardName = retrievedCard?.Name;

            Console.WriteLine($"Card ID: {retrievedCardId}");
            Console.WriteLine($"Card Name: {retrievedCardName}");
        }

        [Test, Order(5)]
        public void CreateCardAndAddDescription()
        {
            RestClient restClient = new RestClient(BaseUrl);
            RestRequest createCardRequest = new RestRequest("cards/");
            createCardRequest.AddQueryParameter("key", apiKey);
            createCardRequest.AddQueryParameter("token", apiToken);
            createCardRequest.AddQueryParameter("name", "Add Description card");
            createCardRequest.AddQueryParameter("idList", todoListId);

            RestResponse createCardResponse = restClient.Post(createCardRequest);
            var cardInfo = JsonConvert.DeserializeObject<Objects>(createCardResponse.Content);
            var cardId = cardInfo?.Id;

            RestRequest addDescriptionRequest = new RestRequest($"cards/{cardId}/desc");
            addDescriptionRequest.AddQueryParameter("key", apiKey);
            addDescriptionRequest.AddQueryParameter("token", apiToken);
            addDescriptionRequest.AddParameter("value", "Connor Trello Description Test");

            RestResponse addDescriptionResponse = restClient.Put(addDescriptionRequest);
            var statusCodeAddDesc = addDescriptionResponse.StatusCode;
            int statCodeAddDesc = (int)statusCodeAddDesc;
            Console.WriteLine("Add Description Status Code: " + statCodeAddDesc);
            Console.WriteLine($"Card '{cardId}' description: '{"Connor Trello Description Test"}'");
        }

        [Test, Order(6)]
        public void CreateNewTODOListNegative()
        {
            RestClient restClient = new RestClient(BaseUrl);
            RestRequest createListRequest = new RestRequest("lists/");
            createListRequest.AddQueryParameter("key", apiKey);
            createListRequest.AddQueryParameter("token", apiToken);
            createListRequest.AddQueryParameter("name", "TODO");
            createListRequest.AddQueryParameter("idBoard", boardId);

            RestResponse createListResponse = restClient.Post(createListRequest);
            var listInfo = JsonConvert.DeserializeObject<Objects>(createListResponse.Content);
            var todoListId = listInfo?.Id;


            RestRequest getListRequest = new RestRequest($"lists/{todoListId}");
            getListRequest.AddQueryParameter("key", apiKey);
            getListRequest.AddQueryParameter("token", apiToken);
            RestResponse getListResponse = restClient.Get(getListRequest);
            string retrievedListDetails = getListResponse.Content.ToString();
            var retrievedList = JsonConvert.DeserializeObject<ListDetails>(retrievedListDetails);
            string retrievedListId = retrievedList?.Id;
            string retrievedListName = retrievedList?.Name;

            Console.WriteLine($"List ID: {retrievedListId}");
            Console.WriteLine($"List Name: {retrievedListName}");
        }
    }
}
