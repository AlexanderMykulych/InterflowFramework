document.addEventListener('DOMContentLoaded', function() {
	var checkPageButton = document.getElementById('checkPage');
	checkPageButton.addEventListener('click', function() {
	    debugger;
	}, false);
}, false);

chrome.runtime.onMessage.addListener(
	function(message, sender, sendResponse) {
		if ( message.from == "content" && message.message == "getTabId")
		{
			var tabId = sender.tab.id;
			sendResponse({ from: 'event', message: '', tabId: tabId });
		}
	}
);