import React, { Fragment, useState } from 'react';
import {
    Loading,
    SearchInputField,
    useSearch,
    utilities,
    Title
} from '@linn-it/linn-form-components-library';
import Typography from '@material-ui/core/Typography';
import PropTypes from 'prop-types';
import Table from '@material-ui/core/Table';
import TableHead from '@material-ui/core/TableHead';
import TableBody from '@material-ui/core/TableBody';
import TableRow from '@material-ui/core/TableRow';
import TableCell from '@material-ui/core/TableCell';

function TypeAheadTable({
    fetchItems,
    table,
    columnNames,
    title,
    loading,
    clearSearch,
    history,
    placeholder
}) {
    const [searchTerm, setSearchTerm] = useState('');
    useSearch(fetchItems, searchTerm, clearSearch);

    const handleSearchTermChange = (_propertyName, newValue) => {
        setSearchTerm(newValue);
    };

    const cursor = {
        cursor: 'pointer',
        textDecoration: 'none'
    };

    const results = () => {
        if (table.rows.length > 0) {
            return (
                <Table>
                    <TableHead>
                        <TableRow>
                            {columnNames.map(columnName => (
                                <TableCell key={columnName}>{columnName}</TableCell>
                            ))}
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {table.rows &&
                            table.rows.map(row => (
                                <Fragment key={row.id}>
                                    <TableRow
                                        style={cursor}
                                        onClick={() => history.push(utilities.getSelfHref(row))}
                                        hover
                                    >
                                        {row.values.map(cell => (
                                            <TableCell key={cell.id} component="th" scope="row">
                                                {cell.value}
                                            </TableCell>
                                        ))}
                                    </TableRow>
                                </Fragment>
                            ))}
                    </TableBody>
                </Table>
            );
        }
        return <Typography>No matching items</Typography>;
    };

    return (
        <Fragment>
            {title && <Title text={title} />}
            <SearchInputField
                placeholder={placeholder}
                onChange={handleSearchTermChange}
                type="search"
                label="Search Works Orders by Part Number"
                variant="outlined"
                value={searchTerm}
                style={{ paddingTop: '8px' }}
            />
            {}
            {loading ? <Loading /> : results()}
        </Fragment>
    );
}

TypeAheadTable.propTypes = {
    table: PropTypes.shape({
        rows: PropTypes.arrayOf(
            PropTypes.shape({
                Id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
                values: PropTypes.arrayOf(
                    PropTypes.shape({
                        id: PropTypes.oneOfType([PropTypes.string, PropTypes.number]),
                        value: PropTypes.string
                    })
                ),
                expandableInfo: PropTypes.shape({
                    Id: PropTypes.string,
                    elements: PropTypes.arrayOf(
                        PropTypes.shape({
                            label: PropTypes.string,
                            value: PropTypes.string
                        })
                    )
                })
            })
        ).isRequired,
        totalItemCount: PropTypes.number.isRequired
    }).isRequired,
    columnNames: PropTypes.arrayOf(PropTypes.string).isRequired,
    history: PropTypes.shape({}).isRequired,
    fetchItems: PropTypes.func.isRequired,
    clearSearch: PropTypes.func.isRequired,
    title: PropTypes.string,
    loading: PropTypes.bool,
    placeholder: PropTypes.string
};

TypeAheadTable.defaultProps = {
    title: null,
    loading: false,
    placeholder: ''
};

export default TypeAheadTable;
