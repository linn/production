import { connect } from 'react-redux';
import {
    getItemErrorDetailMessage,
    initialiseOnMount
} from '@linn-it/linn-form-components-library';
import PurchaseOrder from '../components/PurchaseOrder';
import issueSernosActions from '../actions/issueSernosActions';
import buildSernosActions from '../actions/buildSernosActions';
import purchaseOrderActions from '../actions/purchaseOrderActions';
import purchaseOrderSelectors from '../selectors/purchaseOrderSelectors';
import issueSernosSelectors from '../selectors/issueSernosSelectors';
import * as itemTypes from '../itemTypes';

const mapStateToProps = (state, { match }) => ({
    item: purchaseOrderSelectors.getItem(state),
    itemId: match.params.id,
    editStatus: purchaseOrderSelectors.getEditStatus(state),
    itemLoading: purchaseOrderSelectors.getLoading(state),
    snackbarVisible: issueSernosSelectors.getMessageVisible(state),
    message: issueSernosSelectors.getMessageText(state),
    itemError: getItemErrorDetailMessage(state, itemTypes.purchaseOrder.item)
});

const initialise = ({ itemId }) => dispatch => {
    dispatch(purchaseOrderActions.fetch(itemId));
};

const mapDispatchToProps = {
    initialise,
    issueSernos: issueSernosActions.requestProcessStart,
    buildSernos: buildSernosActions.requestProcessStart,
    setEditStatus: purchaseOrderActions.setEditStatus,
    updatePurchaseOrder: purchaseOrderActions.update
};

export default connect(mapStateToProps, mapDispatchToProps)(initialiseOnMount(PurchaseOrder));
