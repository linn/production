import { connect } from 'react-redux';
import WorksOrdersBatchNotes from '../../components/worksOrders/WorksOrderBatchNotes';
import worksOrdersBatchNotesActions from '../../actions/worksOrdersBatchNotesActions';
import worksOrdersSelectors from '../../selectors/worksOrdersSelectors';

const mapStateToProps = state => ({
    items: worksOrdersSelectors.getSearchItems(state),
    loading: worksOrdersSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: worksOrdersBatchNotesActions.searchWithOptions,
    clearSearch: worksOrdersBatchNotesActions.clearSearch,
    clearErrors: worksOrdersBatchNotesActions.clearErrors
};

export default connect(mapStateToProps, mapDispatchToProps)(WorksOrdersBatchNotes);
