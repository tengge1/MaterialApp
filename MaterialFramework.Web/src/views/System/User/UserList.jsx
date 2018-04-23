import React from 'react';
import {
    withStyles, DataTable, TopBar, SearchForm, Columns, Column, CheckboxColumn, RowNumber,
    Button, TextField, Add, Edit, InfoOutline, Search, Delete
} from '../../../components/Components';
import UserAdd from './UserAdd';

const styles = theme => ({
    root: {
        width: '100%',
        height: '100%'
    }
});

const userData = [{
    id: 1,
    username: 'admin',
    name: '管理员',
    sex: '男',
    role: '超级管理员',
    phone: '12345678',
    imei: '61111111111'
}, {
    id: 2,
    username: 'test',
    name: '测试',
    sex: '女',
    role: '超级管理员',
    phone: '8888888888',
    imei: '3337777777'
}, {
    id: 3,
    username: 'wang',
    name: '王总',
    sex: '男',
    role: '经理',
    phone: '666666666',
    imei: '55555552'
}, {
    id: 4,
    username: 'liu',
    name: '刘经理',
    sex: '男',
    role: '经理',
    phone: '99996666',
    imei: 'fdfd'
}];

var userDatas = [];
for (var i = 0; i < 256; i++) {
    var user = Object.assign({}, userData[i % userData.length]);
    user.id = i + 1;
    user.username = user.username + (parseInt(i / userData.length, 10) + 1);
    user.name = user.name + (parseInt(i / userData.length, 10) + 1);
    userDatas.push(user);
}

class UserList extends React.Component {

    state = {
        searchOpen: false,
        userAddOpen: false,
        userEditOpen: false
    };

    onSearchClick = () => {
        this.setState({
            searchOpen: !this.state.searchOpen
        });
    }

    addUser = () => {
        this.setState({
            userAddOpen: !this.state.userAddOpen
        });
    };

    render() {
        const { classes } = this.props;
        const state = this.state;

        return <React.Fragment>
            <DataTable className={classes.root} data={userDatas} paging={true} searchOpen={state.searchOpen}>
                <TopBar>
                    <Button onClick={this.addUser}>
                        <Add />
                        添加
                    </Button>
                    <Button>
                        <Edit />
                        编辑
                    </Button>
                    <Button>
                        <InfoOutline />
                        查看
                    </Button>
                    <Button onClick={this.onSearchClick}>
                        <Search />
                        查询
                    </Button>
                    <Button>
                        <Delete />
                        删除
                    </Button>
                </TopBar>
                <SearchForm>
                    <TextField label={'用户名/姓名'} />
                    <Button>搜索</Button>
                    <Button>重置</Button>
                </SearchForm>
                <Columns>
                    <CheckboxColumn width={60} />
                    <RowNumber width={60} />
                    <Column name={'username'}>用户名</Column>
                    <Column name={'name'}>姓名</Column>
                    <Column name={'sex'}>性别</Column>
                    <Column name={'role'}>角色</Column>
                    <Column name={'phone'}>手机号</Column>
                    <Column name={'imei'}>手机串号</Column>
                </Columns>
            </DataTable>
            <UserAdd open={state.userAddOpen} />
        </React.Fragment>;
    }
}

export default withStyles(styles)(UserList);