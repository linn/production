import React from 'react';
import TableCell from '@material-ui/core/TableCell';
import PropTypes from 'prop-types';

function TabCell(props) {
    const { children, value, index, ...other } = props;

    if (index !== value) return null;

    return <TableCell {...other}>{children}</TableCell>;
}

TabCell.propTypes = {
    children: PropTypes.node,
    index: PropTypes.number,
    value: PropTypes.number
};

export default TabCell;
