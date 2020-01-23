import { connect } from 'react-redux';
import {
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import LabelPrint from '../../components/labelPrinting/LabelPrint';
import labelPrintActions from '../../actions/labelPrintActions';
import labelPrintTypeActions from '../../actions/labelPrintTypeActions';
import labelPrinterActions from '../../actions/labelPrinterActions';
import * as itemTypes from '../../itemTypes'
import labelPrintTypeSelectors from '../../selectors/labelPrintTypeSelectors';
import labelPrintersSelectors from '../../selectors/labelPrintersSelectors';
import labelPrintSelectors from '../../selectors/labelPrintSelectors';
import addressesActions from '../../actions/addressesActions';
import addressesSelectors from '../../selectors/addressesSelectors';
import suppliersActions from '../../actions/suppliersActions';
import suppliersSelectors from '../../selectors/suppliersSelectors';

const mapStateToProps = state => ({
    loading: labelPrintSelectors.getLoading(state),
    snackbarVisible: labelPrintSelectors.getSnackbarVisible(state),
    itemError: getItemErrorDetailMessage(state, itemTypes.labelPrint.item),
    labelPrintTypes: labelPrintTypeSelectors.getItems(state),
    labelPrinters: labelPrintersSelectors.getItems(state),
    addressSearchLoading: addressesSelectors.getSearchLoading(state),
    addressSearchResults: addressesSelectors.getSearchItems(state),
    //.map(s => ({ ...s, id: s.partNumber, name: s.partNumber }))
    supplierSearchLoading: suppliersSelectors.getSearchLoading(state),
    supplierSearchResults: suppliersSelectors.getSearchItems(state)
});

const initialise = () => dispatch => {
    dispatch(labelPrinterActions.fetch());
    dispatch(labelPrintTypeActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    print: labelPrintActions.add,
    searchAddresses: addressesActions.search,
    clearAddressSearch: addressesActions.clearSearch,
    searchSuppliers: suppliersActions.search,
    clearSupplierSearch: suppliersActions.clearSearch
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(LabelPrint));
