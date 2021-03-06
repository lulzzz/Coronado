import React, { Component } from 'react';
import * as categoryActions from '../../actions/categoryActions';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import CategoryForm from './CategoryForm';
import './CategoryList.css';
import { find } from 'lodash';
import { CategoryRow } from './CategoryRow';
import Spinner from '../common/Spinner';

class CategoryList extends Component {
  constructor(props) {
    super(props);
    this.deleteCategory = this.deleteCategory.bind(this);
    this.startEditing = this.startEditing.bind(this);
    this.handleClose = this.handleClose.bind(this);
    this.saveCategory = this.saveCategory.bind(this);
    this.getCategoryName = this.getCategoryName.bind(this);
    this.state = {
      show: false,
      selectedCategory: {}
    }
  }

  deleteCategory(categoryId, categoryName) {
    this.props.actions.deleteCategory(categoryId, categoryName);
  }

  startEditing(category) {
    this.setState({show:true, selectedCategory: category});
  }

  handleClose() {
    this.setState({show:false});
  }

  saveCategory(category) {
    this.props.actions.updateCategory(category);
  }

  getCategoryName(categoryId) {
    if (!categoryId || categoryId === '') return '';

    var category = find(this.props.categories, c => c.categoryId === categoryId);
    return category ? category.name : '';
  }
  
  render() {
    return (
    <table className='table category-list'>
      <thead>
        <tr>
          <th></th>
          <th>Name</th>
          <th>Type</th>
          <th>Parent</th>
        </tr>
      </thead>
      <tbody>
        <CategoryForm 
          show={this.state.show} 
          onClose={this.handleClose} 
          category={this.state.selectedCategory} 
          categories={this.props.categories}
          onSave={this.saveCategory} />
        { this.props.isLoading ? <tr><td colSpan="5"><Spinner /></td></tr> :
          this.props.categories.map(cat => 
        <CategoryRow 
          key={cat.categoryId} 
          parent={this.getCategoryName(cat.parentCategoryId)}
          category={cat} 
          onEdit={() => this.startEditing(cat)} 
          onDelete={()=>this.deleteCategory(cat.categoryId, cat.name)} />
        )}
      </tbody>
    </table>
    );
  }
}

function mapStateToProps(state) {
  return {
    categories: state.categories,
    notifications: state.notifications,
    isLoading: state.loading.categories
  }
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(categoryActions, dispatch)
  }
}

export default connect(
  mapStateToProps,
  mapDispatchToProps,
)(CategoryList);