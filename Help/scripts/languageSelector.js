

function setActiveTab(baseClass,activeClassName,activeTab) {

  // set defalt for all tabs
  var classElements = getElementsByClass("CodeSnippetContainerTab", null, 'div');
  if(classElements != null) {
    for(i=0; i<classElements.length; i++) {       
 	  classElements[i].style.backgroundColor = "#242424";
 	  classElements[i].style.borderTop = "1px solid #454545";
 	  classElements[i].style.borderBottom = "1px solid #454545";
    }
  }
  var classElements = getElementsByClass("CodeSnippetContainerTabFirst", null, 'div');
  if(classElements != null) {
    for(i=0; i<classElements.length; i++) {       
 	  classElements[i].style.backgroundColor = "#242424";
 	  classElements[i].style.borderTop = "1px solid #454545";
 	  classElements[i].style.borderBottom = "1px solid #454545";
    }
  }
  
  // switch language
  var classElements = getElementsByClass(baseClass, null, 'div');
  if(classElements != null) {
    for(i=0; i<classElements.length; i++) {       
      var pattern = new RegExp("(^|\\s)"+activeClassName+"(\\s|$)");
 	  if ( pattern.test(classElements[i].className) ) {
        classElements[i].style.display = 'block';
	  }
	  else {
        classElements[i].style.display = 'none';
	  }
    }
  }
  // hightlight active tab
  var element = document.getElementById(activeTab);
  if(element != null)
  {
    element.style.backgroundColor = "#242424";
    element.style.borderBottomColor = "#242424";
  } 
}

function getElementsByClass(searchClass,node,tag) {
	var classElements = new Array();
	if ( node == null )
		node = document;
	if ( tag == null )
		tag = '*';
	var els = node.getElementsByTagName(tag);
	var elsLen = els.length;
	var pattern = new RegExp("(^|\\s)"+searchClass+"(\\s|$)");
	for (i = 0, j = 0; i < elsLen; i++) {
		if ( pattern.test(els[i].className) ) {
			classElements[j] = els[i];
			j++;
		}
	}
	return classElements;
}