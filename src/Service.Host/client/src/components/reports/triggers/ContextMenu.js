import React, { Fragment } from 'react';
import IconButton from '@material-ui/core/IconButton';
import Menu from '@material-ui/core/Menu';
import MoreVertIcon from '@material-ui/icons/MoreVert';
import PropTypes from 'prop-types';

function ContextMenu({ children, id }) {
    const [anchorEl, setAnchorEl] = React.useState(null);

    function handleClick(event) {
        setAnchorEl(event.currentTarget);
    }

    function handleClose() {
        setAnchorEl(null);
    }

    return (
        <>
            <IconButton
                aria-label="more"
                aria-controls={id}
                aria-haspopup="true"
                onClick={handleClick}
            >
                <MoreVertIcon />
            </IconButton>
            <Menu
                id={id}
                anchorEl={anchorEl}
                keepMounted
                open={Boolean(anchorEl)}
                onClose={handleClose}
            >
                {children}
            </Menu>
        </>
    );
}

ContextMenu.propTypes = {
    children: PropTypes.arrayOf(PropTypes.shape({})),
    id: PropTypes.string
};

ContextMenu.defaultProps = {
    children: null,
    id: null
};

export default ContextMenu;
