import { connect } from 'react-redux';
import PurchaseOrders from '../components/PurchaseOrders';
import purchaseOrdersActions from '../actions/purchaseOrdersActions';
import purchaseOrdersSelectors from '../selectors/purchaseOrdersSelectors';

const mapStateToProps = state => ({
    items: purchaseOrdersSelectors.getSearchItems(state),
    loading: purchaseOrdersSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: purchaseOrdersActions.search,
    clearSearch: purchaseOrdersActions.clearSearch,
    classes: {}
};

export default connect(mapStateToProps, mapDispatchToProps)(PurchaseOrders);
