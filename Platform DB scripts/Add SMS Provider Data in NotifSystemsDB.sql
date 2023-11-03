INSERT INTO public."SMSProviderEndPoint"(
	"Id", "UrlTemplate", "HttpMethod")
	VALUES (1, 'https://www.4jawaly.net/api/sendsms.php?username=althawaqh&password={0}&message={1}&numbers={2}&sender={3}&unicode=e&return=json&Rmduplicated=1', 0);
	
INSERT INTO public."SMSProvider"(
	"Id", "Sender", "Token", "Rank", "EndpointId")
	VALUES (1, 'AFCO', 'VFcCqWQ9df23v', 1, 1);