import React, { useState, useCallback, Fragment, useEffect } from 'react';
import PropTypes from 'prop-types';
import {
    Dropdown,
    InputField,
    Loading,
    Title,
    ErrorCard,
    SnackbarMessage,
    Typeahead
} from '@linn-it/linn-form-components-library';
import Grid from '@material-ui/core/Grid';
import { makeStyles } from '@material-ui/styles';
import Button from '@material-ui/core/Button';
import Page from '../../containers/Page';

const useStyles = makeStyles(theme => ({
    marginTop: {
        marginTop: theme.spacing(2)
    },
    filtersWithSpacing: {
        marginBottom: theme.spacing(4),
        marginTop: theme.spacing(2)
    },
    boldText: {
        fontWeight: 'bold'
    },
    spacingRight: {
        marginRight: theme.spacing(2)
    },
    hide: {
        display: 'none'
    },
    floatRight: {
        float: 'right'
    }
}));

function LabelPrint({
    loading,
    snackbarVisible,
    setSnackbarVisible,
    message,
    itemError,
    labelPrintTypes,
    labelPrinters,
    print,
    searchAddresses,
    addressSearchLoading,
    addressSearchResults,
    clearAddressSearch,
    supplierSearchLoading,
    supplierSearchResults,
    searchSuppliers,
    clearSupplierSearch,
    getAddressById,
    addressReturnedForId
}) {
    const printLinesInitialState = [
        {
            id: 'supplierId',
            displayName: 'Supplier ID',
            value: '',
            width: 6,
            displayForLabelTypes: [4, 5],
            inputType: 'typeahead'
        },
        {
            id: 'addressId',
            displayName: 'Address ID',
            value: '',
            width: 6,
            displayForLabelTypes: [4, 5],
            inputType: 'typeahead'
        },
        {
            id: 'addressee',
            displayName: 'Addressee',
            value: '',
            width: 12,
            displayForLabelTypes: [4],
            inputType: 'string'
        },
        {
            id: 'addressee2',
            displayName: 'Addressee 2',
            value: '',
            width: 12,
            displayForLabelTypes: [4],
            inputType: 'string'
        },
        {
            id: 'line1',
            displayName: 'Line 1',
            value: '',
            width: 12,
            displayForLabelTypes: [0, 1, 2, 4, 6, 7],
            inputType: 'string'
        },
        {
            id: 'line2',
            displayName: 'Line 2',
            value: '',
            width: 12,
            displayForLabelTypes: [0, 2, 4],
            inputType: 'string'
        },
        {
            id: 'line3',
            displayName: 'Line 3',
            value: '',
            width: 12,
            displayForLabelTypes: [0, 4],
            inputType: 'string'
        },
        {
            id: 'line4',
            displayName: 'Line 4',
            value: '',
            width: 12,
            displayForLabelTypes: [0, 4],
            inputType: 'string'
        },
        {
            id: 'line5',
            displayName: 'Line 5',
            value: new Date().toDateString().slice(4),
            width: 12,
            displayForLabelTypes: [0],
            inputType: 'string'
        },
        {
            id: 'line6',
            displayName: 'Line 6',
            value: '',
            width: 12,
            displayForLabelTypes: [0],
            inputType: 'string'
        },
        {
            id: 'line7',
            displayName: 'Line 7',
            value: '',
            width: 12,
            displayForLabelTypes: [0],
            inputType: 'string'
        },
        {
            id: 'postalCode',
            displayName: 'Postal Code',
            value: '',
            width: 3,
            onOwnLine: true,
            displayForLabelTypes: [4],
            inputType: 'string'
        },
        {
            id: 'country',
            displayName: 'Country',
            value: '',
            width: 9,
            onOwnLine: true,
            displayForLabelTypes: [4],
            inputType: 'string'
        },
        {
            id: 'poNumber',
            displayName: 'PO Number',
            value: '',
            width: 12,
            displayForLabelTypes: [5],
            inputType: 'string'
        },
        {
            id: 'partNumber',
            displayName: 'Part Number',
            value: '',
            width: 12,
            displayForLabelTypes: [5],
            inputType: 'string'
        },
        {
            id: 'qty',
            displayName: 'Qty',
            value: '',
            width: 12,
            displayForLabelTypes: [5],
            inputType: 'number'
        },
        {
            id: 'initials',
            displayName: 'Initials',
            value: '',
            width: 12,
            displayForLabelTypes: [5],
            inputType: 'string'
        },
        {
            id: 'date',
            displayName: 'Date',
            value: new Date().toDateString().slice(4),
            width: 6,
            displayForLabelTypes: [5],
            inputType: 'string',
            onOwnLine: true
        }
    ];

    const [labelType, setLabelType] = useState(0);
    const [printer, setPrinter] = useState(3);
    const [quantity, setQuantity] = useState(1);
    const [labelDetails, setLabelDetails] = useState(printLinesInitialState);
    const [addressReturned, setAddressReturned] = useState(null);

    const classes = useStyles();

    const handleFieldChange = (propertyName, newValue) => {
        if (propertyName === 'printer') {
            setPrinter(newValue);
        }
        if (propertyName === 'quantity') {
            setQuantity(Math.round(newValue));
        }
    };

    const handleLabelTypeChange = (name, newValue) => {
        setLabelType(parseInt(newValue, 10));

        if (newValue === '0' || newValue === '1' || newValue === '4') {
            setPrinter(3); //Large labels & address -> Prodlbl2
        } else if (newValue === '2' || newValue === '6' || newValue === '7') {
            setPrinter(2); //Small labels -> Prodlbl1
        } else if (newValue === '5') {
            setPrinter(0); //address & goods in labels -> goods in 1 GILabels
        }
    };

    const handleLabelDetailsChange = useCallback(
        (lineId, newValue) => {
            const rowToUpdate = labelDetails.findIndex(x => x.id === lineId);
            const updatedDetails = [...labelDetails];
            updatedDetails[rowToUpdate].value = newValue;
            setLabelDetails(updatedDetails);
        },
        [setLabelDetails, labelDetails]
    );

    const handleCopyFromAddress = useCallback(
        newValue => {
            handleLabelDetailsChange('line1', newValue.line1);
            handleLabelDetailsChange('line2', newValue.line2);
            handleLabelDetailsChange('line3', newValue.line3);
            handleLabelDetailsChange('line4', newValue.line4);
            handleLabelDetailsChange('postalCode', newValue.postCode);
            handleLabelDetailsChange('country', newValue.country);
            handleLabelDetailsChange('addressee', newValue.addressee);
            handleLabelDetailsChange('addressee2', newValue.addressee2);
            handleLabelDetailsChange('addressId', newValue.id);
        },
        [handleLabelDetailsChange]
    );

    const handleCopyFromSupplier = newValue => {
        handleLabelDetailsChange('supplierId', newValue.supplierId);
        getAddressById(newValue.orderAddressId);
    };

    useEffect(() => {
        if (addressReturnedForId && addressReturnedForId !== addressReturned) {
            setAddressReturned(addressReturnedForId);
            handleCopyFromAddress(addressReturnedForId);
        }
    }, [addressReturnedForId, addressReturned, handleCopyFromAddress]);

    const handlePrintClick = () => {
        const sendableDetails = {
            SupplierId: `${labelDetails.find(x => x.id === 'supplierId').value}`,
            AddressId: `${labelDetails.find(x => x.id === 'addressId').value}`,
            Addressee: labelDetails.find(x => x.id === 'addressee').value,
            Addressee2: labelDetails.find(x => x.id === 'addressee2').value,
            Line1: labelDetails.find(x => x.id === 'line1').value,
            Line2: labelDetails.find(x => x.id === 'line2').value,
            Line3: labelDetails.find(x => x.id === 'line3').value,
            Line4: labelDetails.find(x => x.id === 'line4').value,
            Line5: labelDetails.find(x => x.id === 'line5').value,
            Line6: labelDetails.find(x => x.id === 'line6').value,
            Line7: labelDetails.find(x => x.id === 'line7').value,
            PostalCode: labelDetails.find(x => x.id === 'postalCode').value,
            Country: labelDetails.find(x => x.id === 'country').value,
            FromPCNumber: `${labelDetails.find(x => x.id === 'fromPCNumber').value}`,
            ToPCNumber: `${labelDetails.find(x => x.id === 'toPCNumber').value}`,
            PoNumber: labelDetails.find(x => x.id === 'poNumber').value,
            PartNumber: labelDetails.find(x => x.id === 'partNumber').value,
            Qty: `${labelDetails.find(x => x.id === 'qty').value}`,
            Initials: labelDetails.find(x => x.id === 'initials').value,
            Date: labelDetails.find(x => x.id === 'date').value
        };

        const printInfo = {
            LabelType: labelType,
            Printer: printer,
            Quantity: quantity,
            LinesForPrinting: sendableDetails
        };
        print(printInfo);
    };

    const handleClearClick = () => {
        setLabelDetails(printLinesInitialState);
    };

    const getInputValue = inputId => {
        const input = labelDetails.find(x => x.id === inputId);
        return input.value;
    };

    const getInputStyle = () => {
        let styleClass = '';
        if (labelType === 7) {
            styleClass = classes.boldText;
        }
        return styleClass;
    };

    return (
        <Fragment>
            <Grid container alignItems="center" justify="center">
                <Grid xs={3} item />
                <Grid xs={6} item>
                    <Page showRequestErrors>
                        <Grid item container>
                            <Grid item xs={12}>
                                <Fragment>
                                    <Button
                                        href="/production/maintenance/labels/reprint-reasons/create"
                                        variant="outlined"
                                        color="primary"
                                        className={classes.floatRight}
                                    >
                                        Reprint label form
                                    </Button>
                                </Fragment>
                                <Title text="General Purpose Label Printer" />
                            </Grid>
                            <SnackbarMessage
                                visible={snackbarVisible}
                                onClose={() => setSnackbarVisible(false)}
                                message={
                                    message && message.data.message ? message.data.message : ''
                                }
                            />
                            <Grid item xs={12}>
                                {itemError && <ErrorCard errorMessage={itemError.errorMessage} />}
                            </Grid>
                            {loading ? (
                                <Grid item xs={12}>
                                    <Loading />
                                </Grid>
                            ) : (
                                <Fragment>
                                    <Grid
                                        item
                                        xs={12}
                                        container
                                        className={classes.filtersWithSpacing}
                                    >
                                        <Grid item xs={4} className={classes.spacingRight}>
                                            <Dropdown
                                                value={labelType || ''}
                                                label="Label Type"
                                                fullWidth
                                                items={labelPrintTypes.map(labelPrintType => ({
                                                    ...labelPrintType,
                                                    id: labelPrintType.id,
                                                    displayText: labelPrintType.name,
                                                    key: labelPrintType.id
                                                }))}
                                                onChange={handleLabelTypeChange}
                                                propertyName="labelType"
                                                allowNoValue={false}
                                            />
                                        </Grid>
                                        <Grid item xs={5} className={classes.spacingRight}>
                                            <Dropdown
                                                value={printer || ''}
                                                label="Printer"
                                                fullWidth
                                                items={labelPrinters.map(labelPrinter => ({
                                                    ...labelPrinter,
                                                    id: labelPrinter.id,
                                                    displayText: labelPrinter.name,
                                                    key: labelPrinter.id
                                                }))}
                                                onChange={handleFieldChange}
                                                propertyName="printer"
                                                allowNoValue={false}
                                            />
                                        </Grid>
                                        <Grid item xs={2}>
                                            <InputField
                                                label="Quantity"
                                                fullWidth
                                                type="number"
                                                onChange={handleFieldChange}
                                                propertyName="quantity"
                                                value={quantity}
                                            />
                                        </Grid>
                                    </Grid>

                                    <Grid
                                        container
                                        xs={12}
                                        className={
                                            labelType === 4 || labelType === 5 ? '' : classes.hide
                                        }
                                    >
                                        <Grid item xs={6}>
                                            <Typeahead
                                                propertyName="supplierId"
                                                label="Supplier Id"
                                                modal
                                                items={supplierSearchResults}
                                                value={getInputValue('supplierId')}
                                                loading={supplierSearchLoading}
                                                fetchItems={searchSuppliers}
                                                links={false}
                                                clearSearch={() => clearSupplierSearch}
                                                placeholder="Search for a Supplier"
                                                onChange={handleLabelDetailsChange}
                                                onSelect={newValue => {
                                                    handleCopyFromSupplier(newValue);
                                                }}
                                            />
                                        </Grid>
                                        <Grid item xs={6}>
                                            <Typeahead
                                                onSelect={newValue => {
                                                    handleCopyFromAddress(newValue);
                                                }}
                                                propertyName="addressId"
                                                label="Address Id"
                                                modal
                                                items={addressSearchResults}
                                                value={getInputValue('addressId')}
                                                loading={addressSearchLoading}
                                                fetchItems={searchAddresses}
                                                links={false}
                                                clearSearch={() => clearAddressSearch}
                                                placeholder="Search for an Address"
                                            />
                                        </Grid>
                                    </Grid>

                                    {labelDetails.map(
                                        line =>
                                            line.displayForLabelTypes.includes(labelType) && (
                                                <Fragment key={line.displayName}>
                                                    <Grid item xs={line.width}>
                                                        {line.inputType !== 'typeahead' && (
                                                            <Fragment>
                                                                <InputField
                                                                    label={line.displayName}
                                                                    fullWidth
                                                                    type={line.inputType}
                                                                    onChange={
                                                                        handleLabelDetailsChange
                                                                    }
                                                                    propertyName={line.id}
                                                                    value={line.value}
                                                                    className={getInputStyle()}
                                                                    key={line.displayName}
                                                                />
                                                            </Fragment>
                                                        )}
                                                    </Grid>
                                                    {line.onOwnLine && <Grid item xs={12} />}
                                                </Fragment>
                                            )
                                    )}
                                    <Grid item xs={6} className={classes.marginTop}>
                                        <Button
                                            onClick={handlePrintClick}
                                            variant="outlined"
                                            className={classes.spacingRight}
                                        >
                                            Print
                                        </Button>
                                        <Button onClick={handleClearClick} variant="outlined">
                                            Clear
                                        </Button>
                                    </Grid>
                                </Fragment>
                            )}
                        </Grid>
                    </Page>
                </Grid>
                <Grid xs={3} item>
                    <img src="https://small.linncdn.com/apps/images/smiley2.png" alt="Smiley" />
                </Grid>
            </Grid>
        </Fragment>
    );
}

LabelPrint.propTypes = {
    loading: PropTypes.bool,
    snackbarVisible: PropTypes.bool,
    itemError: PropTypes.shape({ errorMessage: PropTypes.string }),
    labelPrintTypes: PropTypes.arrayOf(PropTypes.shape({})),
    labelPrinters: PropTypes.arrayOf(PropTypes.shape({})),
    print: PropTypes.func.isRequired,
    message: PropTypes.shape({ data: PropTypes.shape({ message: PropTypes.string }) }),
    searchAddresses: PropTypes.func,
    addressSearchLoading: PropTypes.bool,
    addressSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    clearAddressSearch: PropTypes.func,
    supplierSearchLoading: PropTypes.bool,
    supplierSearchResults: PropTypes.arrayOf(PropTypes.shape({})),
    searchSuppliers: PropTypes.func,
    clearSupplierSearch: PropTypes.func,
    setSnackbarVisible: PropTypes.func,
    getAddressById: PropTypes.func,
    addressReturnedForId: PropTypes.shape({})
};

LabelPrint.defaultProps = {
    loading: false,
    snackbarVisible: false,
    itemError: { errorMessage: '' },
    labelPrintTypes: [{}],
    labelPrinters: [{}],
    message: { data: { message: '' } },
    searchAddresses: null,
    addressSearchLoading: false,
    addressSearchResults: [{}],
    clearAddressSearch: null,
    supplierSearchLoading: false,
    supplierSearchResults: [{}],
    searchSuppliers: null,
    clearSupplierSearch: null,
    setSnackbarVisible: null,
    getAddressById: null,
    addressReturnedForId: {}
};
export default LabelPrint;
