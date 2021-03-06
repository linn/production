import React from 'react';
import Link from '@material-ui/core/Link';
import { Link as RouterLink } from 'react-router-dom';
import PropTypes from 'prop-types';
import makeStyles from '@material-ui/styles/makeStyles';
import TabCell from './TabCell';

const useStyles = makeStyles(theme => ({
    hover: {
        color: theme.palette.primary.main,
        padding: 0,
        cursor: 'pointer',
        textDecoration: 'underline',
        background: theme.palette.action.hover
    },
    link: {
        color: theme.palette.primary.main,
        padding: 0,
        textDecoration: 'none'
    }
}));

function LinkTabCell({ index, value, href, setHoverHref, hoverHref, children }) {
    const classes = useStyles();

    function onMouseEnter() {
        setHoverHref(href);
    }

    function onMouseLeave() {
        setHoverHref(null);
    }

    return (
        <TabCell
            className={href === hoverHref ? classes.hover : classes.link}
            href={href}
            index={index}
            value={value}
            onMouseEnter={onMouseEnter}
            onMouseLeave={onMouseLeave}
        >
            <Link component={RouterLink} to={href}>
                <> {children} </>
            </Link>
        </TabCell>
    );
}

LinkTabCell.propTypes = {
    children: PropTypes.node,
    index: PropTypes.number.isRequired,
    value: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
    href: PropTypes.string,
    hoverHref: PropTypes.string,
    setHoverHref: PropTypes.func.isRequired
};

LinkTabCell.defaultProps = {
    children: null,
    href: null,
    hoverHref: null
};

export default LinkTabCell;
