import React, { Fragment, useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import { makeStyles } from '@material-ui/styles';
import {
    Loading,
    CreateButton,
    ErrorCard,
    Title,
    PaginatedTable
} from '@linn-it/linn-form-components-library';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    root: {
        width: '90%'
    },
    button: {
        marginTop: theme.spacing(1),
        marginRight: theme.spacing(1)
    },
    actionsContainer: {
        marginBottom: theme.spacing(2)
    },
    resetContainer: {
        padding: theme.spacing(3)
    }
}));

const PartFailFaultCodes = ({ loading, itemError, history, items }) => {
    const [pageOptions, setPageOptions] = useState({
        orderBy: '',
        orderAscending: false,
        currentPage: 0,
        rowsPerPage: 10
    });
    const [rowsToDisplay, setRowsToDisplay] = useState([]);

    const classes = useStyles();

    useEffect(() => {
        const rows = items
            ? items.map(el => ({
                  faultCode: `${el.faultCode}`,
                  faultDescription: `${el.faultDescription}`,
                  links: el.links
              }))
            : null;

        if (!rows || rows.length === 0) {
            setRowsToDisplay([]);
        } else {
            setRowsToDisplay(
                rows.slice(
                    pageOptions.currentPage * pageOptions.rowsPerPage,
                    pageOptions.currentPage * pageOptions.rowsPerPage + pageOptions.rowsPerPage
                )
            );
        }
    }, [
        pageOptions.currentPage,
        pageOptions.rowsPerPage,
        pageOptions.orderBy,
        pageOptions.orderAscending,
        items
    ]);

    const handleRowLinkClick = href => history.push(href);

    const columns = {
        faultCode: 'Code',
        faultDescription: 'Description'
    };

    return (
        <Page>
            <Title text="Part Fail Error Types" />
            {itemError && <ErrorCard errorMessage={itemError.statusText} />}
            {loading ? (
                <Loading />
            ) : (
                <Fragment>
                    <Fragment className={classes.actionsContainer}>
                        <CreateButton createUrl="/production/resources/part-fail-fault-codes/create" />
                    </Fragment>
                    <PaginatedTable
                        columns={columns}
                        sortable
                        handleRowLinkClick={handleRowLinkClick}
                        rows={rowsToDisplay}
                        pageOptions={pageOptions}
                        setPageOptions={setPageOptions}
                        totalItemCount={items ? items.length : 0}
                    />
                </Fragment>
            )}
        </Page>
    );
};

PartFailFaultCodes.propTypes = {
    loading: PropTypes.bool.isRequired,
    items: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    itemError: PropTypes.shape({})
};

PartFailFaultCodes.defaultProps = {
    itemError: null,
    items: []
};

export default PartFailFaultCodes;
