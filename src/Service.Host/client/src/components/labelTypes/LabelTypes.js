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

const ViewLabelTypes = ({ loading, itemError, history, items }) => {
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
            labelTypeCode: el.labelTypeCode,
            description: el.description,
            barcodePrefix: el.barcodePrefix,
            nSBarcodePrefix: el.nSBarcodePrefix,
            filename: el.filename,
            defaultPrinter: el.defaultPrinter,
            commandFilename: el.commandFilename,
            testFilename: el.testFilename,
            testPrinter: el.testPrinter,
            testCommandFilename: el.testCommandFilename,
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
        labelTypeCode: 'Label Type Code',
        description: 'Description',
        barcodePrefix: 'Barcode Prefix',
        nSBarcodePrefix: 'NS Barcode Prefix',
        filename: 'Filename',
        defaultPrinter: 'Default Printer',
        commandFilename: 'Command Filename',
        testFilename: 'Test Filename',
        testPrinter: 'Test Printer',
        testCommandFilename: 'Test Command Filename'
    };

    return (
        <Page>
            <Title text="Label Types" />
            {itemError && <ErrorCard errorMessage={itemError.status} />}
            {loading ? (
                <Loading />
            ) : (
                <Fragment>
                    <div className={classes.actionsContainer}>
                        <CreateButton createUrl="/production/resources/label-types/create" />
                    </div>

                    <PaginatedTable
                        columns={columns}
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

ViewLabelTypes.propTypes = {
    loading: PropTypes.bool.isRequired,
    items: PropTypes.arrayOf(PropTypes.shape({})),
    history: PropTypes.shape({ push: PropTypes.func }).isRequired,
    itemError: PropTypes.shape({})
};

ViewLabelTypes.defaultProps = {
    itemError: null,
    items: []
};

export default ViewLabelTypes;
