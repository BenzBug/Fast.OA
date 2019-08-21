var UserManager ={

	Load:function () {
		UserManager.initTable();
		//关闭对话框
		$("#addDialogDiv").css("display", "none");
		$("#editDialogDiv").css("display", "none");

		UserManager.bindSearchBtnClick();
	},

	bindSearchBtnClick:function () {
	    $("#btnSearch").click(function () {
		    var data = { schCode: $("#txtSchCode").val(), schName: $("#txtSchName").val() }
		    UserManager.initTable(data);
	    })
    },

    //初始化表格
	initTable:function (queryParam) {
	$('#tt').datagrid({
		url: '/UserInfo/GetAllUserInfos',//rows:一页有多少条，page：请求当前页
		title: '用户信息列表',
		width: 700,
		height: 510,
		fitColumns: true,
		idField: 'Id',
		loadMsg: '正在加载用户的信息...',
		pagination: true,
		singleSelect: false,
		pageSize: 10,
		pageNumber: 1,
		pageList: [10, 20, 30],
		queryParams: queryParam,//让表格在加载数据的时候，额外传输的数据。
		columns: [[
			{ field: 'ck', checkbox: true, align: 'left', width: 50 },
			{ field: 'Id', title: 'Key', width: 120, hidden: true },
			{ field: 'userCode', title: '用户编码', width: 120 },
			{ field: 'userName', title: '用户名称', width: 120 },
			{ field: 'showName', title: '显示名称', width: 150 },
			{ field: 'pwd', title: '密码', width: 120 },
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
				UserManager.addClickEvent();
			}
		}, {
			id: 'btnDelete',
			text: '删除',
			iconCls: 'icon-cancel',
			handler: UserManager.deleteClickEvent
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
				UserManager.editClickEvent(selectRows[0].Id);
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
				UserManager.editClickEvent($(this).attr("uid"))
			});
			$(".deleteLink").click(function () {
				UserManager.deleteClickEvent()
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
	$("#editFrame").attr("src", "/UserInfo/Edit/" + id);

	$("#editDialogDiv").css("display", "block");
	$("#editDialogDiv").dialog({
		title: "修改用户",
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
	UserManager.initTable();
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

	$.post("/UserInfo/Delete", { ids: ids }, function (data) {
		if (data == "ok") {
			//刷新表格
			UserManager.initTable();
		} else {
			$.messager.alert("错误提醒", "删除失败！", "error");
		}
	});
},

    //当用户点击添加时候，弹出一个对话框
	addClickEvent:function () {
	$("#addDialogDiv").css("display", "block");
	$("#addDialogDiv").dialog({
		title: "添加用户",
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
			handler: UserManager.subAddForm
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
		    UserManager.initTable();
	    }
	    else {
		    $.messager.alert("错误提醒", data, "error");
		    //$.messager.alert(data);
	    }
    }
};

