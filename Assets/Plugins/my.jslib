mergeInto(LibraryManager.library, {

  	Hello: function () {
    	window.alert("Hello, world!");
    	console.log("Hello, world!");
  	},

	SaveDataExtern: function(data) {
    	var dataString = UTF8ToString(data);
    	var myobj = JSON.parse(dataString);
    	player.setData(myobj);
        console.log("Data saved!");
  	},

    SetToLeaderboard: function(value){
        setToLeaderboard(value);
    },

    ShowAdvForBonusMoneyExtern: function() {
        ShowAdvForBonusMoney();
    },

    ShowAdvForFreeMoneyExtern: function() {
        ShowAdvForFreeMoney();
    },

    ShowAdvForFreePowerUpPackExtern: function() {
        ShowAdvForFreePowerUpPack();
    },

    RateGameExtern: function() {
        RateGame();
    },

    GetLang: function() {
        var lang = ysdk.environment.i18n.lang;
        var bufferSize = lengthBytesUTF8(lang) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(lang, buffer, bufferSize);
        return buffer;
    },

    ShowIntAdvExtern: function() {
        ShowIntAdv();
    },

    BuyFirstMoneyPackExtern: function() {
        BuyFirstMoneyPack();
    },

    BuySecondMoneyPackExtern: function() {
        BuySecondMoneyPack();
    },

    BuyThirdMoneyPackExtern: function() {
        BuyThirdMoneyPack();
    },

});