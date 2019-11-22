import { connect } from 'react-redux';
import WorksOrderLabels from '../../components/worksOrders/WorksOrderLabels';
import worksOrderLabelsLabelsActions from '../../actions/worksOrderLabelsActions';
import worksOrderLabelsSelectors from '../../selectors/worksOrderLabelsSelectors';

const mapStateToProps = state => ({
    items: worksOrderLabelsSelectors.getSearchItems(state),
    loading: worksOrderLabelsSelectors.getSearchLoading(state)
});

const mapDispatchToProps = {
    fetchItems: worksOrderLabelsLabelsActions.search,
    clearSearch: worksOrderLabelsLabelsActions.clearSearch,
    clearErrors: worksOrderLabelsLabelsActions.clearErrors
};

export default connect(mapStateToProps, mapDispatchToProps)(WorksOrderLabels);
