var dialog = {
    background: "#777",
    opacity: "0.6",
    msgWidth: 550,
    msgHeight: 265,
    borderColor: "#336699",
    titleHeight: 25,
    titleColor: "#99CCFF",
    alert: function (innerHTML) {
        var sWidth, sHeight;
        sWidth = document.body.offsetWidth;
        sHeight = screen.height;

        var bgObj = document.createElement("div");
        bgObj.setAttribute('id', 'bgDiv');
        bgObj.style.position = "absolute";
        bgObj.style.top = "0";
        bgObj.style.background = this.background;
        bgObj.style.filter = "progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75";
        bgObj.style.opacity = this.opacity;
        bgObj.style.left = "0";
        bgObj.style.width = sWidth + "px";
        bgObj.style.height = sHeight + "px";
        bgObj.style.zIndex = "10000";
        document.body.appendChild(bgObj);

        var msgObj = document.createElement("div")
        msgObj.setAttribute("id", "msgDiv");
        msgObj.setAttribute("align", "center");
        msgObj.style.background = "white";
        msgObj.style.border = "1px solid " + this.borderColor;
        msgObj.style.position = "absolute";
        msgObj.style.left = "50%";
        msgObj.style.top = "50%";
        msgObj.style.font = "12px/1.6em Verdana, Geneva, Arial, Helvetica, sans-serif";
        msgObj.style.marginLeft = "-225px";
        msgObj.style.marginTop = -75 + document.documentElement.scrollTop + "px";
        msgObj.style.width = this.msgWidth + "px";
        msgObj.style.height = this.msgHeight + "px";
        msgObj.style.textAlign = "center";
        msgObj.style.lineHeight = "25px";
        msgObj.style.zIndex = "10001";

        var title = document.createElement("h4");
        title.setAttribute("id", "msgTitle");
        title.setAttribute("align", "right");
        title.style.margin = "0";
        title.style.padding = "3px";
        title.style.background = this.borderColor;
        title.style.filter = "progid:DXImageTransform.Microsoft.Alpha(startX=20, startY=20, finishX=100, finishY=100,style=1,opacity=75,finishOpacity=100);";
        title.style.opacity = "0.75";
        title.style.border = "1px solid " + this.borderColor;
        title.style.height = "18px";
        title.style.font = "12px Verdana, Geneva, Arial, Helvetica, sans-serif";
        title.style.color = "white";
        title.style.cursor = "pointer";
        title.innerHTML = "关闭窗口";
        title.onclick = function () {
            document.body.removeChild(bgObj);
            document.getElementById("msgDiv").removeChild(title);
            document.body.removeChild(msgObj);
        }
        document.body.appendChild(msgObj);
        document.getElementById("msgDiv").appendChild(title);

        var txt = document.createElement("p");
        txt.style.margin = "1em 0"
        txt.setAttribute("id", "msgTxt");
        txt.innerHTML = innerHTML;
        document.getElementById("msgDiv").appendChild(txt);
    }
};

