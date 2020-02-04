import { connect } from 'react-redux';
import {
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import PurchaseOrder from '../components/PurchaseOrder';
import issueSernosActions from '../actions/issueSernosActions';
import purchaseOrderActions from '../actions/purchaseOrderActions';
import purchaseOrderSelectors from '../selectors/purchaseOrderSelectors';
import issueSernosSelectors from '../selectors/issueSernosSelectors';
import * as itemTypes from '../itemTypes';

const mapStateToProps = state => ({
    item: purchaseOrderSelectors.getItem(state),
    itemLoading: purchaseOrderSelectors.getLoading(state),
    snackbarVisible: issueSernosSelectors.getSnackbarVisible(state),
    message: issueSernosSelectors.getItem(state),
    itemError: getItemErrorDetailMessage(state, itemTypes.purchaseOrder.item),
});

const initialise = () => dispatch => {
    dispatch(purchaseOrderActions.fetch());
};

const mapDispatchToProps = {
    initialise,
    issueSernos: issueSernosActions.requestProcessStart
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PurchaseOrder));
