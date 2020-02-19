import React from 'react';
import List from '@material-ui/core/List';
import PropTypes from 'prop-types';

function FactList({ children, id }) {
    return <List id={id}>{children}</List>;
}

FactList.propTypes = {
    children: PropTypes.arrayOf(PropTypes.shape({})),
    id: PropTypes.shape({})
};

FactList.defaultProps = {
    children: null,
    id: null
};

export default FactList;
