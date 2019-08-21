var ActionManager ={

	Load:function () {
	    ActionManager.initTable();
		//关闭对话框
		$("#addDialogDiv").css("display", "none");
		$("#editDialogDiv").css("display", "none");
		$("#trIconUrl").css("display", "none");

		ActionManager.bindSearchBtnClick();
	    //绑定  上传图片按钮的点击事件
		ActionManager.bindUploagBtnClick();
	    //绑定是否菜单按钮
		ActionManager.bindCkbIsMenuClick();

	},

	bindSearchBtnClick:function () {
	    $("#btnSearch").click(function () {
		    var data = { schName: $("#txtSchName").val() }
		    ActionManager.initTable(data);
	    })
    },

    //绑定一个上传图片的按钮点击事件
	bindUploagBtnClick: function () {
	    $("#btnUploadFile").click(function () {
	        $("#addDialogDiv form").ajaxSubmit({
	            url: "/ActionInfo/UploadImg",
	            type: "Post",
	            succeess: function (data) {
	                //把返回的url地址放到页面的隐藏域
	                $("#MenuIcon").val(data);
	                $("#uploadImg").html("<img src='"+data+"' style='width:40px; height:40px;'/>");
	            }
	        });
	    });
	},

	bindCkbIsMenuClick: function () {
	    $("#ckbIsMenu").change(function () {
	        //显示或者隐藏那个  图标地址
	        $("#trIconUrl").toggle();
	    });
	},

    //初始化表格
	initTable:function (queryParam) {
	$('#tt').datagrid({
	    url: '/ActionInfo/GetAllActionInfos',//rows:一页有多少条，page：请求当前页
		title: '权限信息列表',
		width: 900,
		height: 510,
		fitColumns: true,
		idField: 'Id',
		loadMsg: '正在加载权限信息...',
		pagination: true,
		singleSelect: false,
		pageSize: 10,
		pageNumber: 1,
		pageList: [10, 20, 30],
		queryParams: queryParam,//让表格在加载数据的时候，额外传输的数据。
		columns: [[
			{ field: 'ck', checkbox: true, align: 'left', width: 50 },
			{ field: 'Id', title: 'Key', width: 120, hidden: true },
		    { field: 'ActionName', title: '权限名称', width: 150 },
            { field: 'Url', title: 'URL地址', width: 250 },
            { field: 'HttpMethd', title: 'Http方法', width: 140 },
            {
                field: 'IsMenu', title: '是否菜单', width: 130,
                formatter: function (value, row, index) {
                    return value ? "是" : "否";
                }
	        },
            {
                field: 'MenuIcon', title: '菜单图片', width: 130,
                formatter: function (value, row, index) {
                    return "<img src='" + value + "' width='20px' height='20px' />";
                }
            },
            { field: 'Sort', title: '排序', width: 100 },
            { field: 'remark', title: '备注', width: 120 },
			{
				field: 'createTime', title: '创建时间', width: 220, align: 'right',
				formatter: function (value, row, index) {
					return (eval(value.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"))).pattern("yyyy-M-d h:m:s");
				}
			},
			{
				field: 'ModfiedOn', title: '操作', width: 160, formatter: function (value, row, index) {
					var str = "";
					str += "<a href='javascript:void(0)' class='editLink' uid='" + row.Id + "'>修改</a> &nbsp;&nbsp;";
					str += "<a href='javascript:void(0)' class='deleteLink' uid='" + row.Id + "'>删除</a>";
					return str;
				}
			}
		]],
		toolbar: [{
			id: 'btnDownShelf',
			text: '添加',
			iconCls: 'icon-add',
			handler: function () {
				//alert("dd");
				//弹出一个添加的对话框
				ActionManager.addClickEvent();
			}
		}, {
			id: 'btnDelete',
			text: '删除',
			iconCls: 'icon-cancel',
			handler: ActionManager.deleteClickEvent
		}, {
			id: 'btnEdit',
			text: '修改',
			iconCls: 'icon-edit',
			handler: function () {
				//校验是否只选中了一个用户
				var selectRows = $("#tt").datagrid("getSelections");
				if (selectRows.length != 1) {
					$.messager.alert("错误提醒", "请选中一行需要修改的数据", "question");
					return;
				}
				ActionManager.editClickEvent(selectRows[0].Id);
			}

		}, {
			id: 'btnSetRole',
			text: '设置角色',
			iconCls: 'icon-redo',
			handler: function () {
				//判断是否选中一个用户进行角色设置。弹出一个设置角色的对话框出来。
				setRole();
			}
		}, {
			id: 'btnSetAction',
			text: '设置特殊权限',
			iconCls: 'icon-redo',
			handler: function () {
				//判断是否选中一个用户进行角色设置。弹出一个设置角色的对话框出来。
				setAction();
			}
		}],
		onLoadSuccess: function (data) {
			//每列后面的【修改】【删除】按钮
			$(".editLink").click(function () {
				ActionManager.editClickEvent($(this).attr("uid"))
			});
			$(".deleteLink").click(function () {
				ActionManager.deleteClickEvent()
			});
		}
	});
},
    ////当用户点击修改时候
    //function editCheckEvent() {
    //    editClickEvent(selectRows[0].Id);
    //}

    //弹出修改界面
	editClickEvent:function (id) {
	//给editFrame  的src属性做一个赋值
	    $("#editFrame").attr("src", "/ActionInfo/Edit/" + id);

	$("#editDialogDiv").css("display", "block");
	$("#editDialogDiv").dialog({
		title: "修改权限",
		modal: true,
		width: 400,
		height: 400,
		collapsible: true,
		minimizable: true,
		maximizable: true,
		resizable: true,
		buttons: [{
			id: 'btnOk',
			text: '修改',
			iconCls: 'icon-ok',
			handler: function () {
				$("#editFrame")[0].contentWindow.submitForm();
			}
		}, {
			id: 'btnCancel',
			text: '取消',
			iconCls: 'icon-cancel',
			handler: function () {
				$("#editDialogDiv").dialog("close");
			}
		}]
	});
},
    //修改成功后由子容器调用的方法
	afterEditSuccess:function () {
	$("#editDialogDiv").dialog("close");
	ActionManager.initTable();
},

    //当用户点击删除时候
	deleteClickEvent:function () {
	//第一步，拿到easyUI里面选中的项
	var selectRows = $('#tt').datagrid("getSelections");
	if (selectRows.length <= 0) {
		$.messager.alert("错误提醒", "请选中删除数据！", "warning");
		return;
	}
	var ids = "";
	for (var key in selectRows) {
		ids += selectRows[key].Id + ",";
	}
	ids = ids.trimRight(',');

	$.post("/ActionInfo/Delete", { ids: ids }, function (data) {
		if (data == "ok") {
			//刷新表格
			ActionManager.initTable();
		} else {
			$.messager.alert("错误提醒", "删除失败！", "error");
		}
	});
},

    //当用户点击添加时候，弹出一个对话框
	addClickEvent:function () {
	$("#addDialogDiv").css("display", "block");
	$("#addDialogDiv").dialog({
		title: "添加权限",
		modal: true,
		width: 400,
		height: 400,
		collapsible: true,
		minimizable: true,
		maximizable: true,
		resizable: true,
		buttons: [{
			id: 'btnOk',
			text: '确定',
			iconCls: 'icon-ok',
			handler: ActionManager.subAddForm
		}, {
			id: 'btnCancel',
			text: '取消',
			iconCls: 'icon-cancel',
			handler: function () {
				$("#addDialogDiv").dialog("close");
			}
		}]

	});

},
    //把表单提交到后台。
	subAddForm:function () {
	$("#addDialogDiv form").submit();
},
    //添加成功过之后执行的代码
    afterAdd:function (data) {
	    if (data == "ok") {
		    //关闭对话框刷新表格
		    $("#addDialogDiv").dialog("close");
		    ActionManager.initTable();
	    }
	    else {
		    $.messager.alert("错误提醒", data, "error");
		    //$.messager.alert(data);
	    }
    }
};

