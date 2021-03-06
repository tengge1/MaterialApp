import React from 'react';
import classNames from 'classnames';
import { withStyles } from 'material-ui';
import CardActions from '../card/CardActions.jsx';
import Paper from '../paper/Paper.jsx';
import Checkbox from '../form/Checkbox.jsx';
import Table from './Table.jsx';
import TableHead from './TableHead.jsx';
import TableBody from './TableBody.jsx';
import TableRow from './TableRow.jsx';
import TableCell from './TableCell.jsx';
import TableFooter from './TableFooter.jsx';
import TablePagination from './TablePagination.jsx';
import TablePaginationActions from './TablePaginationActions.jsx';
import Collapse from '../transition/Collapse.jsx';

import TopBar from '../placeholder/TopBar.jsx';
import BottomBar from '../placeholder/BottomBar.jsx';
import Columns from '../placeholder/Columns.jsx';
import SearchForm from '../placeholder/SearchForm.jsx';
import Column from '../placeholder/Column.jsx';
import CheckboxColumn from '../placeholder/CheckboxColumn.jsx';
import RowNumber from '../placeholder/RowNumber.jsx';

const styles = theme => ({
    root: {

    },
    headPaper: {
        position: 'sticky',
        borderTop: '1px solid #ddd'
    },
    bodyPaper: {
        maxHeight: 'calc(100% - 56px - 56px - 56px - 2px)',
        overflow: 'auto'
    },
    footerPaper: {
        position: 'sticky',
        bottom: 0
    },
    cardActions: {
        // padding: '3px 6px'
    },
    subTableContainer: {
        overflow: 'auto'
    }
});

class DataTable extends React.Component {

    state = {
        time: new Date().getTime()
    };

    idProperty = 'id';

    rowsPerPage = 10; // 每页的行数
    page = 0; // 第几页（从0开始计数）
    count = 0; // 记录总条数
    rows = []; // 每行的数据
    selected = []; // 当前选中的记录id

    handleRowClick = (rowId) => {
        const index = this.selected.indexOf(rowId);
        if (index === -1) {
            this.selected.push(rowId);
        } else {
            this.selected.splice(index, 1);
        }
        this.refresh();
    };

    handleSelectChange = () => {
        const { onSelectChange } = this.props;
        if (onSelectChange) {
            const selected = this.rows.filter((row) => {
                return this.selected.indexOf(row.id) > -1;
            });
            onSelectChange(selected);
        }
    };

    handleDoubleClick = (rowId) => {
        const { onDoubleClick } = this.props;
        if (onDoubleClick) {
            var _this = this;
            const rowData = this.rows.filter(function (row) {
                return row[_this.idProperty] === rowId;
            })[0];
            onDoubleClick(rowData);
        }
    }

    refresh = () => { // 刷新数据
        this.setState({
            time: new Date().getTime()
        });
    }

    onChangePage = (event, page) => {
        this.page = page;
        this.refresh();
    }

    onChangeRowsPerPage = (event) => {
        this.rowsPerPage = event.target.value;
        this.handleSelectChange();
        this.refresh();
    }

    onSelectAllClick = (event, checked) => {
        const rows = this.rows.slice(this.rowsPerPage * this.page, this.rowsPerPage * (this.page + 1));

        if (checked) {
            rows.forEach((n) => {
                if (this.selected.indexOf(n[this.idProperty]) === -1) {
                    this.selected.push(n[this.idProperty]);
                }
            });
        } else {
            this.selected = [];
        }
        event.stopPropagation();
        this.handleSelectChange();
        this.refresh();
    }

    onSelectClick = (event, checked, id) => {
        if (checked) {
            if (this.selected.indexOf(id) === -1) {
                this.selected.push(id);
            }
            // else {
            //     throw new Error('GridPanel: duplicate selected id.');
            // }
        } else {
            var index = this.selected.indexOf(id);
            if (index > -1) {
                this.selected.splice(index, 1);
            }
        }
        event.stopPropagation();
        this.refresh();
    }

    parseTopBar = (n) => {
        const { classes } = this.props;
        return <CardActions className={classes.cardActions}>{n.props.children}</CardActions>;
    }

    parseBottomBar = (n) => {
        const { classes } = this.props;
        return <CardActions className={classes.cardActions}>{n.props.children}</CardActions>;
    }

    parseSearchForm = (n) => {
        const { searchOpen, classes } = this.props;
        return <Collapse in={searchOpen}><CardActions className={classes.cardActions}>{n.props.children}</CardActions></Collapse >;
    }

    parseTableHead = (head) => {
        const rows = this.rows.slice(this.rowsPerPage * this.page, this.rowsPerPage * (this.page + 1));

        var totalWidth = 0;
        head.props.children.forEach((n) => {
            if (n.props.width) {
                totalWidth += n.props.width;
            } else {
                totalWidth += 100;
            }
        });

        const headRow = <TableRow>{head.props.children.map((n, index) => {
            const width = n.props.width ? n.props.width : 100;
            if (n.type === CheckboxColumn) {
                return <TableCell
                    width={parseInt(width / totalWidth * 100, 10) + '%'}
                    padding={'checkbox'}
                    key={index}>
                    <Checkbox
                        indeterminate={this.selected.length > 0 && this.selected.length < rows.length}
                        checked={this.selected.length > 0 && this.selected.length === rows.length}
                        onChange={this.onSelectAllClick} />
                </TableCell>;
            } else if (n.type === RowNumber) {
                return <TableCell
                    width={parseInt(width / totalWidth * 100, 10) + '%'}
                    padding={'checkbox'}
                    key={index}></TableCell>;
            } else if (n.type === Column) {
                return <TableCell
                    width={parseInt(width / totalWidth * 100, 10) + '%'}
                    key={index}>
                    {n.props.children}
                </TableCell>;
            } else {
                return <TableCell
                    width={parseInt(width / totalWidth * 100, 10) + '%'}
                ></TableCell>;
            }
        })}</TableRow>;

        return <TableHead>{headRow}</TableHead>;
    }

    parseTableBody = (head) => {
        const { classes, searchOpen } = this.props;

        const rows = this.rows.slice(this.rowsPerPage * this.page, this.rowsPerPage * (this.page + 1));

        var totalWidth = 0;
        head.props.children.forEach((n) => {
            if (n.props.width) {
                totalWidth += n.props.width;
            } else {
                totalWidth += 100;
            }
        });

        const tableRows = rows.map((row, rowIndex) => {
            return <TableRow
                hover={true}
                selected={rowIndex % 2 === 0 ? true : false}
                key={rowIndex}
                onClick={() => this.handleRowClick(row[this.idProperty])}
                onDoubleClick={() => this.handleDoubleClick(row[this.idProperty])}>{head.props.children.map((col, colIndex) => {
                    const width = col.props.width ? col.props.width : 100;
                    if (col.type === CheckboxColumn) {
                        return <TableCell
                            width={parseInt(width / totalWidth * 100, 10) + '%'}
                            padding={'checkbox'}
                            key={colIndex}>
                            <Checkbox
                                checked={this.selected.indexOf(row[this.idProperty]) > -1}
                                onChange={(event, checked) => this.onSelectClick(event, checked, row[this.idProperty])} />
                        </TableCell>;
                    } else if (col.type === RowNumber) {
                        return <TableCell
                            width={parseInt(width / totalWidth * 100, 10) + '%'}
                            padding={'checkbox'}
                            key={colIndex}>
                            {this.rowsPerPage * this.page + rowIndex + 1}
                        </TableCell>;
                    } else if (col.type === Column) {
                        return <TableCell
                            width={parseInt(width / totalWidth * 100, 10) + '%'}
                            key={colIndex}>
                            {row[col.props.name]}
                        </TableCell>;
                    } else {
                        return <TableCell
                            width={parseInt(width / totalWidth * 100, 10) + '%'}
                        ></TableCell>;
                    }
                })}</TableRow>;
        });

        const content = <div className={classes.subTableContainer}><Table><TableBody>{tableRows}</TableBody></Table></div>;

        return <TableBody>
            <TableRow>
                <TableCell colSpan={head.props.children.length} style={{ padding: searchOpen ? '0 0 64px 0' : 0 }}>{content}</TableCell>
            </TableRow>
        </TableBody>;
    }

    render() {
        const { classes, className, children, data } = this.props;

        this.count = data == null ? 0 : data.length;
        this.rows = data == null ? [] : data;

        var topBar = null;
        var tableHead = null;
        var searchForm = null;
        var tableBody = null;
        var bottomBar = null;

        if (children != null) {
            (Array.isArray(children) ? children : [children]).forEach((n) => {
                if (n.type === TopBar) {
                    topBar = this.parseTopBar(n);
                } else if (n.type === BottomBar) {
                    bottomBar = this.parseBottomBar(n);
                } else if (n.type === SearchForm) {
                    searchForm = this.parseSearchForm(n);
                } else if (n.type === Columns) {
                    tableHead = this.parseTableHead(n);
                    tableBody = this.parseTableBody(n);
                } else {
                    console.log(`DataTable: 不支持的子要素类型：${n.type.name}`);
                }
            });
        }

        var tableFooter =
            <TableFooter>
                {bottomBar == null ? null : <TableRow>{bottomBar}</TableRow>}
                <TableRow>
                    <TablePagination
                        count={this.count}
                        page={this.page}
                        rowsPerPage={this.rowsPerPage}
                        Actions={TablePaginationActions}
                        onChangePage={this.onChangePage}
                        onChangeRowsPerPage={this.onChangeRowsPerPage}
                    />
                </TableRow>
            </TableFooter>;

        return (
            <Paper className={classNames(classes.root, className)}>
                {topBar}
                {searchForm}
                <Paper className={classes.headPaper} elevation={0}>
                    <Table>
                        {tableHead}
                    </Table>
                </Paper>
                <Paper className={classes.bodyPaper}>
                    <Table>
                        {tableBody}
                    </Table>
                </Paper>
                <Paper className={classes.footerPaper} elevation={1}>
                    <Table>
                        {tableFooter}
                    </Table>
                </Paper>
            </Paper>
        );
    }
}

export default withStyles(styles)(DataTable);