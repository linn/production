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

const ViewManufacturingResources = ({ loading, errorMessage, history, items }) => {
    const [pageOptions, setPageOptions] = useState({
        orderBy: '',
        orderAscending: false,
        currentPage: 0,
        rowsPerPage: 10
    });
    const [rowsToDisplay, setRowsToDisplay] = useState([]);

    const classes = useStyles();

    useEffect(() => {
        const compare = (field, orderAscending) => (a, b) => {
            if (!field) {
                return 0;
            }

            if (a[field] < b[field]) {
                return orderAscending ? -1 : 1;
            }

            if (a[field] > b[field]) {
                return orderAscending ? 1 : -1;
            }

            return 0;
        };

        const rows = items.map(el => ({
            resourceCode: el.resourceCode,
            description: el.description,
            cost: el.cost,
            links: el.links
        }));

        if (!rows || rows.length === 0) {
            setRowsToDisplay([]);
        } else {
            setRowsToDisplay(
                rows
                    .sort(compare(pageOptions.orderBy, pageOptions.orderAscending))
                    .slice(
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
        resourceCode: 'Resource Code',
        description: 'Description',
        cost: 'Cost'
    };

    return (
        <Page>
            <Title text="Manufacturing resources" />
            {errorMessage && <ErrorCard errorMessage={errorMessage} />}
            {loading ? (
                <Loading />
            ) : (
                <Fragment>
                    <Fragment className={classes.actionsContainer}>
                        <CreateButton createUrl="/production/resources/manufacturing-resources/create" />
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

ViewManufacturingResources.propTypes = {
    loading: PropTypes.bool.isRequired,
    items: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    errorMessage: PropTypes.string
};

ViewManufacturingResources.defaultProps = {
    errorMessage: '',
    items: []
};

export default ViewManufacturingResources;
