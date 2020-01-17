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

const mapStateToProps = state => ({
    loading: labelPrintSelectors.getLoading(state),
    snackbarVisible: labelPrintSelectors.getSnackbarVisible(state),
    itemError: getItemErrorDetailMessage(state, itemTypes.labelPrint.item),
    labelPrintTypes: labelPrintTypeSelectors.getItems(state),
    labelPrinters: labelPrintersSelectors.getItems(state)
});

const initialise = () => dispatch => {
    dispatch(labelPrinterActions.fetch());
    dispatch(labelPrintTypeActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    print: labelPrintActions.add
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(LabelPrint));
