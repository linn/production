import React from 'react';
import TableCell from '@material-ui/core/TableCell';
import PropTypes from 'prop-types';

function TabCell(props) {
    const { children, value, index } = props;

    if (index !== value) return null;

    return <TableCell>{children}</TableCell>;
}

TabCell.propTypes = {
    children: PropTypes.node,
    index: PropTypes.number,
    value: PropTypes.number
};

TabCell.defaultProps = {
    children: null,
    index: null,
    value: null
};

export default TabCell;
